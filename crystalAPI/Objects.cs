namespace Crystal {
public record Lattice(string Name, int AtomsInUnitCell, int CoordinationNum, double APF, int[] CPD, int[] CPP);

public record CubicLattice(string Name, int AtomsInUnitCell, int CoordinationNum, double EdgeLengthToRadiusRatio, double APF, int[] CPD, int[] CPP) : Lattice(Name,AtomsInUnitCell,CoordinationNum,APF,CPD,CPP);

public record TetragonalLattice(string Name, int AtomsInUnitCell, int CoordinationNum, double APF, int[] CPD, int[] CPP,double EdgeLengthToRadiusRatio) : Lattice(Name,AtomsInUnitCell,CoordinationNum,APF,CPD,CPP);

public record HexagonalLattice(string Name, int AtomsInUnitCell, int CoordinationNum, double APF, int[] CPD, int[] CPP,double EdgeLengthToRadiusRatio) : Lattice(Name,AtomsInUnitCell,CoordinationNum,APF,CPD,CPP);

public record TrigonalLattice(string Name, int AtomsInUnitCell, int CoordinationNum, double APF, int[] CPD, int[] CPP,double EdgeLengthToRadiusRatio) : Lattice(Name,AtomsInUnitCell,CoordinationNum,APF,CPD,CPP);

public record MonoclinicLattice(string Name, int AtomsInUnitCell, int CoordinationNum, double APF, int[] CPD, int[] CPP,double EdgeLengthToRadiusRatio) : Lattice(Name,AtomsInUnitCell,CoordinationNum,APF,CPD,CPP);

public record TriclinicLattice(string Name, int AtomsInUnitCell, int CoordinationNum, double APF, int[] CPD, int[] CPP,double EdgeLengthToRadiusRatio) : Lattice(Name,AtomsInUnitCell,CoordinationNum,APF,CPD,CPP);


public record AtomType(int Num,double Mass,double Radius, Lattice LatticeType) {
    // Avogadro's number (units in 1 gram)
    const double N_A = 6.02214076e23;
    // Picometer to centimeter relation
    const double pmTocm = 1e-10;
    // Mass density of atom within its own boundaries (g/cm^3)
    public double PTheo => 3*Mass/(N_A*4*Math.PI*Math.Pow(Radius*pmTocm,3));
    // Crystal structure mass density (g/cm^3)
    public double P => LatticeType.APF*PTheo;

}

public record CrystalMassDensityInfo(double Density,string IsItPossible);


public record Crystal
{
    public AtomType Atom {get; init;}

    public CubicLattice Lattice {get; init;}

    public Crystal(AtomType atom,CubicLattice lattice) {
        Atom = atom;
        Lattice = lattice;
    }

}

}