using System;

namespace ship_parts {
    public enum deflector_type {
        kOne,
        kTwo,
        kThree,
        kNone
    };

    public class deflector { 
        private deflector_type type = deflector_type.kNone;
        private int health = 0;
        private int photon_health = 0;

        public deflector() {
            health = 0;
        }

        public deflector(deflector_type T, bool is_photon) {
            type = T;
            switch (type) {
                case deflector_type.kOne:
                    health = 2;
                    break;
                case deflector_type.kTwo:
                    health = 10;
                    break;
                case deflector_type.kThree:
                    health = 40;
                    break;
            }
            if (is_photon) {
                photon_health = 3;
            }
        }

        static public deflector CreateDeflector(deflector_type T, bool is_photon) {
            deflector res = new deflector(T, is_photon);

            return res;
        }

        public bool GetDamage(space_objects.Object Obj, bool has_anti_nitrin) {
            if (Obj.GetDmg() == 40 && has_anti_nitrin) {
                return false;
            }

            if (!Obj.GetPhoton())  {
                this.health -= Obj.GetDmg();
                if (this.health < 0)
                {
                    return true;
                }
            } else {
                this.photon_health--;
                if (photon_health < 0)
                {
                    return true;
                }
            }
            return false;
        }

        public void ZeroHealth() {
            health = 0;
        }

        public int GetPhotonHealth() { 
            return photon_health; 
        }
        public int GetHealth() {
            return health;
        }
    }

    public enum body_type {
        kOne,
        kTwo,
        kThree
    }

    public class body { 
        private body_type type = body_type.kOne;
        private int health = 1;
        private bool is_valid = false;

        public body() {
            health = 1;
            is_valid = true;
        }

        public body(body_type T) {
            type = T;
            switch (type)
            {
                case body_type.kOne:
                    health = 1;
                    break;
                case body_type.kTwo:
                    health = 5;
                    break;
                case body_type.kThree:
                    health = 20;
                    break;
            }

            is_valid = true;
        }

        static public body CreateBody(body_type T) {
            body res = new body(T);

            return res;
        }

        public int GetHealth() {
            return this.health;
        }

        public void SetHealth(int new_health) {
            this.health = new_health;
        }

        public void GetDamage(int damage) {
            this.health -= damage;
        }
    }

    public enum engine_type { 
        kC,
        kE,
        kAlpha,
        kOmega,
        kGamma,
        kNone
    };

    public enum environment_type { 
        space,
        fog,
        nitrin_parts
    };

    public class engine {
        private engine_type type = engine_type.kNone;
        private bool is_jumping = false;
        private double fuel_intake = 0;
        private double start_cost = 0;
        private double dist_jump = 0;

        engine(engine_type T) {
            type = T;

            switch (type)
            {
                case engine_type.kC:
                    fuel_intake = 1;
                    start_cost = 1;
                    break;
                case engine_type.kE:
                    fuel_intake = 1.5f;
                    start_cost = 1;
                    break;
                case engine_type.kAlpha:
                    is_jumping = true;
                    dist_jump = 5;
                    break;
                case engine_type.kOmega:
                    is_jumping = true;
                    dist_jump = 10;
                    break;
                case engine_type.kGamma:
                    is_jumping = true;
                    dist_jump = 15;
                    break;
            };
        }

        static public engine CreateEngine(engine_type T) {
            engine res = new engine(T);

            return res;
        }

        public double GetJumpMaxDistance() {
            return dist_jump;
        }

        public engine_type GetEngType() {
            return type;
        }

        public double GetStartCost() {
            return start_cost;
        }

        public double CountFuelNeededForPath( lab1_y26_oop.part_of_route por ) {
            double res = 0;

            switch (type) {
                case engine_type.kC:
                    res += fuel_intake * por.GetDist();
                    break;
                case engine_type.kE:
                    res += fuel_intake * por.GetDist();
                    break;
                case engine_type.kAlpha:
                    res += por.GetDist();
                    break;
                case engine_type.kOmega:
                    res += por.GetDist() * Math.Log(por.GetDist(), 2);
                    break;
                case engine_type.kGamma:
                    res += por.GetDist() * por.GetDist();
                    break;
            }

            return res;
        }
    }
}