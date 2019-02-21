using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AveryanovCoursework3
{
    class Formules
    {

        public double R_a(double a, double q, double l) =>
            q * a / 6 * (3 - 2 * a / l);

        public double R_b(double a, double q, double l) =>
            q * Math.Pow(a, 2) / 3 / l;

        public double getGreaterR(double a, double M0, double l) =>
            R_a(a, M0, l) > R_b(a, M0, l) ? R_a(a, M0, l) : R_b(a, M0, l);

        public double Q()=>
            0;

        public double Q_max() =>
            0;

        public double M(double M0) => 
            -M0;

        public double M_max(double M0) =>
            M0;

        public double f() =>
            1;
        public double Jx(double b, double h, double d = 0) =>
            b * Math.Pow(h, 3) / 12 - 2 * 0.141 * Math.Pow(d, 4);

        public double w( double M0, double l, double E, double Jx, double z) =>
            -(M0 * Math.Pow(l, 2) / (2 * E * Jx)) * Math.Pow((1 - (z / l)),2);

        public double o(double M0, double l, double E, double Jx, double z) =>
            ( M0 * z ) / ( E * Jx );

        public double stress(double M_max, double y, double Jx) =>
            M_max * y / Jx;

        public double mass(double l, double b, double h, double p, double d = 0) =>
            (b * h - 3 * Math.PI * Math.Pow(d / 2, 2)) * l * p;

        public double b(double step, double b_0, double h, double d = 0) =>
            12 * (step / h * Jx(b_0, h) + 3 * Math.PI * Math.Pow(d, 4) / 64) / Math.Pow(step, 3);
    }
}
