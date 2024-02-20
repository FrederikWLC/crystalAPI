namespace Crystal {
public record Lattice(string Name, int AtomsInUnitCell, int CoordinationNum, double APF, int[] CPD, int[] CPP);

public record CubicLattice(string Name, int AtomsInUnitCell, int CoordinationNum, double EdgeLengthToRadiusRatio, double APF, int[] CPD, int[] CPP) : Lattice(Name,AtomsInUnitCell,CoordinationNum,APF,CPD,CPP);

public record TetragonalLattice(string Name, int AtomsInUnitCell, int CoordinationNum, double APF, int[] CPD, int[] CPP,double EdgeLengthToRadiusRatio) : Lattice(Name,AtomsInUnitCell,CoordinationNum,APF,CPD,CPP);

public record HexagonalLattice(string Name, int AtomsInUnitCell, int CoordinationNum, double APF, int[] CPD, int[] CPP,double EdgeLengthToRadiusRatio) : Lattice(Name,AtomsInUnitCell,CoordinationNum,APF,CPD,CPP);

public record TrigonalLattice(string Name, int AtomsInUnitCell, int CoordinationNum, double APF, int[] CPD, int[] CPP,double EdgeLengthToRadiusRatio) : Lattice(Name,AtomsInUnitCell,CoordinationNum,APF,CPD,CPP);

public record MonoclinicLattice(string Name, int AtomsInUnitCell, int CoordinationNum, double APF, int[] CPD, int[] CPP,double EdgeLengthToRadiusRatio) : Lattice(Name,AtomsInUnitCell,CoordinationNum,APF,CPD,CPP);

public record TriclinicLattice(string Name, int AtomsInUnitCell, int CoordinationNum, double APF, int[] CPD, int[] CPP,double EdgeLengthToRadiusRatio) : Lattice(Name,AtomsInUnitCell,CoordinationNum,APF,CPD,CPP);


public record AtomType(int Num,double Mass,double Radius) {
    public Double PTheo => 3*Mass/(4*Math.PI*Math.Pow(Radius,3));
}

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