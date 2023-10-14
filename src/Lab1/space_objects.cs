namespace space_objects
{
    public class Object
    {
        private int dmg = 0;
        private bool is_photon = false;

        public int GetDmg()
        {
            return this.dmg;
        }

        public void SetDmg(int newdmg)
        {
            dmg = newdmg;
        }

        public bool GetPhoton()
        {
            return this.is_photon;
        }

        public void SetPhoton(bool flag)
        {
            is_photon = flag;
        }
    }

    class asteroid : Object
    {
        public asteroid()
        {
            this.SetDmg(1);
            this.SetPhoton(false);
        }
    }

    class meteor : Object
    {
        public meteor()
        {
            this.SetDmg(4);
            this.SetPhoton(false);
        }
    }

    class whale : Object
    {
        public whale()
        {
            this.SetDmg(40);
            this.SetPhoton(false);
        }
    }

    class antimateria : Object
    {
        public antimateria()
        {
            this.SetDmg(0);
            this.SetPhoton(true);
        }
    }
}