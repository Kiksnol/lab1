using System.Collections.Generic;

namespace ship_parts {
    public enum mass_type {
        none,
        small,
        medium,
        huge
    };

    public enum ship_type {
        none,
        shuttle,
        vaklas,
        meredian,
        stella,
        avgur
    };

    
    public class spaceship {
        private mass_type MT = mass_type.none;
        private deflector Def;
        private engine 
            Eng,
            JumpEng;

        private bool start_flag = false, is_counted = false;

        private bool 
            has_jump_engine = false, 
            has_anti_nitrin = false;
        private body Body;
        private ship_type shiptype = ship_type.none;
        private double 
            total_flight_fuel_plasm = 0,
            total_flight_fuel_materia = 0;

        private double 
            price_per_1_plasm = 1,
            price_per_1_materia = 2;
        public double total_flight_cost = 0;

        public spaceship( ship_type ST, bool is_photon_defense) {
            shiptype = ST;

            switch (ST) {
                case ship_type.stella:
                    Def = deflector.CreateDeflector(deflector_type.kOne, is_photon_defense);
                    Body = body.CreateBody(body_type.kOne);
                    MT = mass_type.small;
                    Eng = engine.CreateEngine(engine_type.kC);
                    has_jump_engine = true;
                    JumpEng = engine.CreateEngine(engine_type.kOmega);
                    break;
                case ship_type.avgur:
                    Def = deflector.CreateDeflector(deflector_type.kThree, is_photon_defense);
                    Body = body.CreateBody(body_type.kThree);
                    MT = mass_type.huge;
                    Eng = engine.CreateEngine(engine_type.kE);
                    has_jump_engine = true;
                    JumpEng = engine.CreateEngine(engine_type.kAlpha);
                    break;
                case ship_type.vaklas:
                    Def = deflector.CreateDeflector(deflector_type.kNone, is_photon_defense);
                    Body = body.CreateBody(body_type.kTwo);
                    MT = mass_type.medium;
                    Eng = engine.CreateEngine(engine_type.kE);
                    has_jump_engine = true;
                    JumpEng = engine.CreateEngine(engine_type.kGamma);
                    break;
                case ship_type.shuttle:
                    Def = deflector.CreateDeflector(deflector_type.kNone, is_photon_defense);
                    Body = body.CreateBody(body_type.kOne);
                    MT = mass_type.small;
                    Eng = engine.CreateEngine(engine_type.kC);
                    break;
                case ship_type.meredian:
                    Def = deflector.CreateDeflector(deflector_type.kTwo, is_photon_defense);
                    Body = body.CreateBody(body_type.kTwo);
                    MT = mass_type.medium;
                    Eng = engine.CreateEngine(engine_type.kE);
                    has_anti_nitrin = true;
                    break;
            }
        }

        public ship_type GetShipType() { 
            return shiptype;
        }

        private bool GetSpaceshipDamage(space_objects.Object Obj) {
            if (Def.GetDamage(Obj, has_anti_nitrin))
            {
                if (Def.GetPhotonHealth() < 0)
                {
                    return false;
                }
                else if (Def.GetHealth() < 0)
                {
                    Body.GetDamage(-Def.GetHealth());
                    Def.ZeroHealth();
                    if (Body.GetHealth() <= 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool Run(List<lab1_y26_oop.part_of_route> Route) {
            foreach (var val in Route) {
                switch (val.GetEnvType())
                {
                    case environment_type.fog:
                        if (!has_jump_engine)
                        {
                            return false;
                        }
                        else
                        {
                            if (val.GetDist() <= JumpEng.GetJumpMaxDistance())
                            {
                                total_flight_fuel_materia += JumpEng.CountFuelNeededForPath(val);
                            }
                            else
                            {
                                return false;
                            }

                            if (val.GetObj().GetPhoton())
                            {
                                if (!GetSpaceshipDamage(val.GetObj()))
                                {
                                    return false;
                                }
                            }
                        }
                        break;
                    case environment_type.space:
                        total_flight_fuel_plasm += Eng.CountFuelNeededForPath(val);
                        start_flag = true;
                        if (val.GetObj().GetDmg() != 40 && !val.GetObj().GetPhoton())
                        {
                            if (!GetSpaceshipDamage(val.GetObj()))
                            {
                                return false;
                            }
                        }

                        break;
                    case environment_type.nitrin_parts:
                        if (Eng.GetEngType() == engine_type.kC)
                        {
                            total_flight_fuel_plasm += 2 * Eng.CountFuelNeededForPath(val);
                        }
                        else
                        {
                            total_flight_fuel_plasm += Eng.CountFuelNeededForPath(val);
                        }

                        if (val.GetObj().GetDmg() == 40) {
                            if (!GetSpaceshipDamage(val.GetObj()))
                            {
                                return false;
                            }
                        }


                        start_flag = true;
                        break;
                }

                if (start_flag && !is_counted)
                {
                    total_flight_fuel_plasm += Eng.GetStartCost();
                    is_counted = true;
                }
            }

            total_flight_cost += total_flight_fuel_materia * price_per_1_materia + total_flight_fuel_plasm * price_per_1_plasm;

            return true;
        }
    };
}