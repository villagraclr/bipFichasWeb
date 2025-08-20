using System;

namespace MDS.Core.Util
{
    public class FormatoRut
    {
        public String getRutFormato(String rut)
        {
            String[] FRut = new String[5];

            if (rut.Length == 9)
            {
                //144927990
                FRut[0] = rut.Substring(0, 2);
                FRut[1] = rut.Substring(2, 3);
                FRut[2] = rut.Substring(5, 3);
                FRut[3] = rut.Substring(8, 1);
            }

            if (rut.Length == 8)
            {//74517895
                FRut[0] = rut.Substring(0, 1);
                FRut[1] = rut.Substring(1, 3);
                FRut[2] = rut.Substring(4, 3);
                FRut[3] = rut.Substring(7, 1);
            }
            return FRut[0] + "." + FRut[1] + "." + FRut[2] + "-" + FRut[3];
        }
    }
}
