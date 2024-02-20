using Crystal;
using System.Text.Json;

JsonElement LoadJSONData(string fileName) {
    using StreamReader r = File.OpenText(fileName);
    string json = r.ReadToEnd();
    JsonDocument jsonDocument = JsonDocument.Parse(json);
    JsonElement root = jsonDocument.RootElement;
    return root;
}

JsonElement pTableData = LoadJSONData("pTable.json").GetProperty("pTable");

var CubicLatticeTypes = new[]
{
    new Crystal.CubicLattice("Simple Cubic",1,6,2,Math.PI/6,[1,0,0],[1,0,0]), 
    new Crystal.CubicLattice("Body Centered Cubic",2,8,4/Math.Sqrt(3),Math.PI*Math.Sqrt(3)/8,[1,1,1],[1,1,0]),
    new Crystal.CubicLattice("Face Centered Cubic",4,12,4/Math.Sqrt(2),Math.PI*Math.Sqrt(2)/6,[1,1,0],[1,1,1])
};

Lattice UnknownLatticeType = new Lattice("Unknown",0,0,1,[0,0,0],[0,0,0]);

var TetragonalLatticeTypes = new[]
{
    "Simple Tetragonal", "Body Centered Tetragonal"
};

var OrthorhombicLatticeTypes = new[]
{
    "Simple Orthorhombic", "Body Centered Orthorhombic","Face Centered Orthorhombic","Side Centered Orthorhombic"
};

var HexagonalLatticeTypes = new[]
{
    "Simple Hexagonal"
};

var MonoclinicLatticeTypes = new[]
{
    "Simple Monoclinic","Side Centered Monoclinic"
};

var TriclinicLatticeTypes = new[]
{
    "Simple Triclinic"
};

string getStructAbbrev(string structName) {
    Dictionary<string, string> StructNamesToAbbrevs = new Dictionary<string, string> { { "Simple Cubic", "sc"}, { "Body Centered Cubic", "bcc" }, { "Face Centered Cubic", "fcc" } };
    string? Abbrev;
    StructNamesToAbbrevs.TryGetValue(structName, out Abbrev);
    return Abbrev != null? Abbrev : "unknown";
}

Lattice getLatticeObject(string abbrev) {
    Dictionary<string, Lattice> StructAbbrevsToObject = new Dictionary<string, Lattice> { {"sc", CubicLatticeTypes[0]}, { "bcc", CubicLatticeTypes[1] }, { "fcc", CubicLatticeTypes[2] } };
    Lattice? LatticeType;
    StructAbbrevsToObject.TryGetValue(abbrev, out LatticeType);
    return LatticeType != null? LatticeType : UnknownLatticeType;
}

AtomType[] AtomTypes = new AtomType[pTableData.GetArrayLength()];

int index = 0;
foreach (JsonElement atom in pTableData.EnumerateArray()) {
    
    if (atom.TryGetProperty("atomic_number", out JsonElement number) && atom.TryGetProperty("atomic_mass", out JsonElement mass) && atom.TryGetProperty("radius", out JsonElement radiusObject)) {
    if (radiusObject.TryGetProperty("empirical", out JsonElement radius) && atom.TryGetProperty("crystal_structure", out JsonElement structure)) {

    
    Console.WriteLine($"Number: {number}| Mass {mass} | Radius {radius} | Structure {structure}");

    AtomTypes[index] = new AtomType(number.GetInt32(),mass.GetDouble(),radius.GetDouble(),getLatticeObject(getStructAbbrev(structure.GetString())));
    } else {break;}}
    index ++;

}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapGet("/atoms", () =>
{
    return AtomTypes;
})
.WithName("GetAtoms")
.WithOpenApi();

app.MapGet("/cubes", () =>
{
    return CubicLatticeTypes;
})
.WithName("GetCubicLattices")
.WithOpenApi();

app.MapGet("/cubicCrystalMassDensity", (int atomicNum,string cubicType) =>
{   
    new Dictionary<string, CubicLattice> { { "sc", CubicLatticeTypes[0]}, { "bcc", CubicLatticeTypes[1] }, { "fcc", CubicLatticeTypes[2] } }.TryGetValue(cubicType,out CubicLattice? CubicType);
    AtomType Atom = AtomTypes[atomicNum-1];
    double P = CubicType.APF*Atom.PTheo;
    return P;
})
.WithName("GetCubicCrystalMassDensity")
.WithOpenApi();


app.MapGet("/ptable", () =>
{
    return LoadJSONData("pTable.json");
})
.WithName("GetPTable")
.WithOpenApi();

app.Run();


