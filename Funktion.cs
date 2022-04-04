using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace kundenfeedbackanwendung_varena
{
    public class Funktion
    {
        public Funktion()
        {

        }

        public static int WertVergeben(int wert, RadioButton r1, RadioButton r2, RadioButton r3, RadioButton r4)
        {
            if (r1.Checked == true)
            {
                return 1;
            }
            else if (r2.Checked == true)
            {
                return 2;
            }
            else if (r3.Checked == true)
            {
                return 3;
            }
            else if (r4.Checked == true)
            {
                return 4;
            }
            else
            {
                return wert;
            }
        }

        public static int WertVergebenList(int wert, RadioButtonList rbl)
        {
            if (rbl.Items[0].Selected == true)
            {
                return 1;
            }
            else if (rbl.Items[1].Selected == true)
            {
                return 2;
            }
            else if (rbl.Items[2].Selected == true)
            {
                return 3;
            }
            else if (rbl.Items[3].Selected == true)
            {
                return 4;
            }
            else if (rbl.Items[4].Selected == true)
            {
                return 5;
            }
            else
            {
                return wert;
            }
        }

        public static int WertVergebenListA(int wert, RadioButtonList rbl)
        {
            if (rbl.Items[0].Selected == true)
            {
                return 1;
            }
            else if (rbl.Items[1].Selected == true)
            {
                return 2;
            }
            else if (rbl.Items[2].Selected == true)
            {
                return 3;
            }
            else if (rbl.Items[3].Selected == true)
            {
                return 4;
            }
            else
            {
                return wert;
            }
        }

        public static char WertVergebenListG(char wert, RadioButtonList rbl)
        {
            if (rbl.Items[0].Selected == true)
            {
                return 'm';
            }
            else if (rbl.Items[1].Selected == true)
            {
                return 'w';
            }
            else
            {
                return wert;
            }
        }

        public static void Uncheck(int wert, RadioButton r1, RadioButton r2, RadioButton r3, RadioButton r4)
        {
            switch (wert)
            {
                case 1:
                    r1.Checked = false;
                    break;
                case 2:
                    r2.Checked = false;
                    break;
                case 3:
                    r3.Checked = false;
                    break;
                case 4:
                    r4.Checked = false;
                    break;
            }
        }

        public static void UncheckList(int wert, RadioButtonList rbl)
        {
            switch (wert)
            {
                case 1:
                    rbl.Items[0].Selected = false;
                    break;
                case 2:
                    rbl.Items[1].Selected = false;
                    break;
                case 3:
                    rbl.Items[2].Selected = false;
                    break;
                case 4:
                    rbl.Items[3].Selected = false;
                    break;
                case 5:
                    rbl.Items[4].Selected = false;
                    break;
            }
        }

        public static void UncheckListA(int wert, RadioButtonList rbl)
        {
            switch (wert)
            {
                case 1:
                    rbl.Items[0].Selected = false;
                    break;
                case 2:
                    rbl.Items[1].Selected = false;
                    break;
                case 3:
                    rbl.Items[2].Selected = false;
                    break;
                case 4:
                    rbl.Items[3].Selected = false;
                    break;
            }
        }

        public static void UncheckListG(int wert, RadioButtonList rbl)
        {
            switch (wert)
            {
                case 'm':
                    rbl.Items[0].Selected = false;
                    break;
                case 'w':
                    rbl.Items[1].Selected = false;
                    break;
            }
        }

        public static void Check(int wert, RadioButton r1, RadioButton r2, RadioButton r3, RadioButton r4)
        {
            switch (wert)
            {
                case 1:
                    r1.Checked = true;
                    break;
                case 2:
                    r2.Checked = true;
                    break;
                case 3:
                    r3.Checked = true;
                    break;
                case 4:
                    r4.Checked = true;
                    break;
            }
        }
        public static void CheckList(int wert, RadioButtonList rbl)
        {
            switch (wert)
            {
                case 1:
                    rbl.Items[0].Selected = true;
                    break;
                case 2:
                    rbl.Items[1].Selected = true;
                    break;
                case 3:
                    rbl.Items[2].Selected = true;
                    break;
                case 4:
                    rbl.Items[3].Selected = true;
                    break;
                case 5:
                    rbl.Items[4].Selected = true;
                    break;
            }
        }

        public static void CheckListA(int wert, RadioButtonList rbl)
        {
            switch (wert)
            {
                case 1:
                    rbl.Items[0].Selected = true;
                    break;
                case 2:
                    rbl.Items[1].Selected = true;
                    break;
                case 3:
                    rbl.Items[2].Selected = true;
                    break;
                case 4:
                    rbl.Items[3].Selected = true;
                    break;
            }
        }

        public static void CheckListG(int wert, RadioButtonList rbl)
        {
            switch (wert)
            {
                case 'm':
                    rbl.Items[0].Selected = true;
                    break;
                case 'w':
                    rbl.Items[1].Selected = true;
                    break;

            }
        }
    }
}