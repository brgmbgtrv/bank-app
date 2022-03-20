using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bogatyrev_Project_2
{
    class Money : IEquatable<Money>, IComparable
    {
        private bool sign;
        private int ipart;
        private byte fpart;

        public bool Sign    // #1 and #3 (AM1 and MS1)
        {
            get
            {
                return sign;
            }
            private set
            {
                sign = value;
            }
        }

        public int Ipart    // #1 and #3 (AM2 and MS2)
        {
            get
            {
                return ipart;
            }
            set
            {
                if (value >= 0)
                {
                    ipart = value;
                }
            }
        }

        public byte Fpart    // #1 and #3 (AM3 and MS3)
        {
            get
            {
                return fpart;
            }
            set
            {
                if (value >= 0 & value <= 99)
                {
                    fpart = value;
                }
            }
        }

        public override string ToString()    // #2 (by Object.ToString() overriding)
        {
            if (sign)
            {
                if (this.fpart < 10)
                {
                    return $"{ipart},0{fpart}";
                }
                else
                {
                    return $"{ipart},{fpart}";
                }
            }
            else
            {
                if (this.fpart < 10)
                {
                    return $"-{ipart},0{fpart}";
                }
                else
                {
                    return $"-{ipart},{fpart}";
                }
            }
        }

        public void Set(string value)    // #3 (MS4)
        {
            if (value[0] == '-')
            {
                sign = false;
                ipart = int.Parse(value.Substring(1, value.Length - 3));
                fpart = byte.Parse(value.Substring(value.Length - 2, value.Length - 1));
            }
            else
            {
                sign = true;
                ipart = int.Parse(value.Substring(0, value.Length - 3));
                fpart = byte.Parse(value.Substring(value.Length - 2, value.Length - 1));
            }
        }

        Random rnd = new Random();
        public Money()    // #4
        {
            sign = true;
            ipart = 0;
            fpart = 0;
        }

        public Money(bool val_1, int val_2, byte val_3)    //  #5 (C2)
        {
            sign = val_1;
            ipart = val_2;
            fpart = val_3;
        }

        public Money(Money obj)    // #5 (C3)
        {
            sign = obj.sign;
            ipart = obj.ipart;
            fpart = obj.fpart;
        }

        public Money(string value)    // #5 (C4)
        {
            if (value[0] == '-')
            {
                sign = false;
                if (value.Contains(",") || value.Contains("."))
                {
                    ipart = int.Parse(value.Substring(0, value.Length - 3));
                    fpart = byte.Parse(value.Substring(value.Length - 2, 2));
                }
                else
                {
                    ipart = int.Parse(value);
                    fpart = 0;
                }
            }
            else
            {
                sign = true;
                if (value.Contains(",") || value.Contains("."))
                {
                    ipart = int.Parse(value.Substring(0, value.Length - 3));
                    fpart = byte.Parse(value.Substring(value.Length - 2, 2));
                } else
                {
                    ipart = int.Parse(value);
                    fpart = 0;
                }             
            }
        }

        public void MAdd1(bool val_1, int val_2, byte val_3)    // #6
        {
            if (val_1)
            {
                if (this.sign)    // a + b
                {
                    this.ipart += val_2;
                    if (this.fpart + val_3 > 99)
                    {
                        this.ipart++;
                        this.fpart = Convert.ToByte(this.fpart + val_3 - 100);
                    }
                    else
                    {
                        this.fpart += val_3;
                    }
                }
                else    // -a + b
                {
                    if (val_2 - this.ipart >= 0)    // -a + b >= 0
                    {
                        this.sign = true;
                        this.ipart = -this.ipart + val_2;
                        if (this.fpart <= val_3)
                        {
                            this.fpart = Convert.ToByte(val_3 - this.fpart);
                        }
                        else
                        {
                            this.ipart--;
                            this.fpart = Convert.ToByte(100 - (val_3 - this.fpart));
                        }
                    }
                    else    // -a + b < 0
                    {
                        this.ipart = -(-this.ipart + val_2);
                        if (this.fpart < val_3)
                        {
                            this.ipart--;
                            this.fpart = Convert.ToByte(100 - (val_3 - this.fpart));
                        }
                        else
                        {
                            this.fpart = Convert.ToByte(val_3 - this.fpart);
                        }
                    }
                }
            }
            else
            {
                if (this.sign)    // a + -b
                {
                    if (this.ipart - val_2 < 0)    // a + -b < 0
                    {
                        this.sign = false;
                        this.ipart = -(this.ipart - val_2);
                        if (this.fpart > val_3)
                        {
                            this.ipart--;
                            this.fpart = Convert.ToByte(100 - (this.fpart - val_3));
                        }
                        else
                        {
                            this.fpart = Convert.ToByte(val_3 - this.fpart);
                        }
                    }
                    else    // a + -b >= 0
                    {
                        this.ipart -= val_2;
                        if (this.fpart > val_3)
                        {
                            this.fpart -= val_3;
                        }
                        else
                        {
                            if (this.ipart == 0 & val_2 == 0)
                            {
                                this.sign = false;
                                this.fpart = Convert.ToByte((-(this.fpart) + val_3));
                            }
                            else
                            {
                                this.ipart--;
                                this.fpart = Convert.ToByte(100 - (val_3 - this.fpart));
                            }
                        }
                    }
                }
                else    // -a + -b
                {
                    this.ipart += val_2;
                    if (this.fpart + val_3 > 99)
                    {
                        this.ipart++;
                        this.fpart = Convert.ToByte(this.fpart + val_3 - 100);
                    }
                    else
                    {
                        this.fpart += val_3;
                    }
                }
            }
        }

        public void MAdd2(Money obj)    // #7
        {
            if (obj.sign)
            {
                if (this.sign)    // a + b
                {
                    this.ipart += obj.ipart;
                    if (this.fpart + obj.fpart > 99)
                    {
                        this.ipart++;
                        this.fpart = Convert.ToByte(this.fpart + obj.fpart - 100);
                    }
                    else
                    {
                        this.fpart += obj.fpart;
                    }
                }
                else    // -a + b
                {
                    if (obj.ipart - this.ipart >= 0)    // -a + b >= 0
                    {
                        this.sign = true;
                        this.ipart = -this.ipart + obj.ipart;
                        if (this.fpart <= obj.fpart)
                        {
                            this.fpart = Convert.ToByte(obj.fpart - this.fpart);
                        }
                        else
                        {
                            this.ipart--;
                            this.fpart = Convert.ToByte(100 - (this.fpart - obj.fpart));
                        }
                    }
                    else    // -a + b < 0
                    {
                        this.ipart = -(-this.ipart + obj.ipart);
                        if (this.fpart < obj.fpart)
                        {
                            this.ipart--;
                            this.fpart = Convert.ToByte(100 - (obj.fpart - this.fpart));
                        }
                        else
                        {
                            this.fpart = Convert.ToByte(obj.fpart - this.fpart);
                        }
                    }
                }
            }
            else
            {
                if (this.sign)    // a + -b
                {
                    if (this.ipart - obj.ipart < 0)    // a + -b < 0
                    {
                        this.sign = false;
                        this.ipart = -(this.ipart - obj.ipart);
                        if (this.fpart > obj.fpart)
                        {
                            this.ipart--;
                            this.fpart = Convert.ToByte(100 - (this.fpart - obj.fpart));
                        }
                        else
                        {
                            this.fpart = Convert.ToByte(obj.fpart - this.fpart);
                        }
                    }
                    else    // a + -b >= 0
                    {
                        this.ipart -= obj.ipart;
                        if (this.fpart > obj.fpart)
                        {
                            this.fpart -= obj.fpart;
                        }
                        else
                        {
                            if (this.ipart == 0 & obj.ipart == 0)
                            {
                                this.sign = false;
                                this.fpart = Convert.ToByte((-(this.fpart) + obj.fpart));
                            }
                            else
                            {
                                this.ipart--;
                                this.fpart = Convert.ToByte(100 - (obj.fpart - this.fpart));
                            }
                        }
                    }
                }
                else    // -a + -b
                {
                    this.ipart += obj.ipart;
                    if (this.fpart + obj.fpart > 99)
                    {
                        this.ipart++;
                        this.fpart = Convert.ToByte(this.fpart + obj.fpart - 100);
                    }
                    else
                    {
                        this.fpart += obj.fpart;
                    }
                }
            }
        }

        public void MSub1(bool val_1, int val_2, byte val_3)    // #8
        {
            if (val_1)
            {
                if (this.sign)    // a - b
                {
                    if (this.ipart - val_2 < 0)    // a + -b < 0
                    {
                        this.sign = false;
                        this.ipart = -(this.ipart - val_2);
                        if (this.fpart > val_3)
                        {
                            this.ipart--;
                            this.fpart = Convert.ToByte(100 - (this.fpart - val_3));
                        }
                        else
                        {
                            this.fpart = Convert.ToByte(val_3 - this.fpart);
                        }
                    }
                    else    // a + -b >= 0
                    {
                        this.ipart -= val_2;
                        if (this.fpart > val_3)
                        {
                            this.fpart -= val_3;
                        }
                        else
                        {
                            if (this.ipart == 0 & val_2 == 0)
                            {
                                this.sign = false;
                                this.fpart = Convert.ToByte((-(this.fpart) + val_3));
                            }
                            else
                            {
                                this.ipart--;
                                this.fpart = Convert.ToByte(100 - (val_3 - this.fpart));
                            }
                        }
                    }
                }
                else    // -a - b (same as -a + -b) 
                {
                    this.ipart += val_2;
                    if (this.fpart + val_3 > 99)
                    {
                        this.ipart++;
                        this.fpart = Convert.ToByte(this.fpart + val_3 - 100);
                    }
                    else
                    {
                        this.fpart += val_3;
                    }
                }
            }
            else
            {
                if (this.sign)    // a - -b (same as a + b)
                {
                    this.ipart += val_2;
                    if (this.fpart + val_3 > 99)
                    {
                        this.ipart++;
                        this.fpart = Convert.ToByte(this.fpart + val_3 - 100);
                    }
                    else
                    {
                        this.fpart += val_3;
                    }
                }
                else    // -a - -b (same as -a + b)
                {
                    if (val_2 - this.ipart >= 0)    // -a + b >= 0
                    {
                        this.sign = true;
                        this.ipart = -this.ipart + val_2;
                        if (this.fpart <= val_3)
                        {
                            this.fpart = Convert.ToByte(val_3 - this.fpart);
                        }
                        else
                        {
                            this.ipart--;
                            this.fpart = Convert.ToByte(100 - (val_3 - this.fpart));
                        }
                    }
                    else    // -a + b < 0
                    {
                        this.ipart = -(-this.ipart + val_2);
                        if (this.fpart < val_3)
                        {
                            this.ipart--;
                            this.fpart = Convert.ToByte(100 - (val_3 - this.fpart));
                        }
                        else
                        {
                            this.fpart = Convert.ToByte(val_3 - this.fpart);
                        }
                    }
                }
            }
        }

        public void MSub2(Money obj)    // #9
        {
            if (obj.sign)
            {
                if (this.sign)    // a - b (same as a + -b)
                {
                    if (this.ipart - obj.ipart < 0)    // a + -b < 0
                    {
                        this.sign = false;
                        this.ipart = -(this.ipart - obj.ipart);
                        if (this.fpart > obj.fpart)
                        {
                            this.ipart--;
                            this.fpart = Convert.ToByte(100 - (this.fpart - obj.fpart));
                        }
                        else
                        {
                            this.fpart = Convert.ToByte(obj.fpart - this.fpart);
                        }
                    }
                    else    // a + -b >= 0
                    {
                        this.ipart -= obj.ipart;
                        if (this.fpart > obj.fpart)
                        {
                            this.fpart -= obj.fpart;
                        }
                        else
                        {
                            if (this.ipart == 0 & obj.ipart == 0)
                            {
                                this.sign = false;
                                this.fpart = Convert.ToByte((-(this.fpart) + obj.fpart));
                            }
                            else
                            {
                                this.ipart--;
                                this.fpart = Convert.ToByte(100 - (obj.fpart - this.fpart));
                            }
                        }
                    }
                }
                else    // -a + -b
                {
                    this.ipart += obj.ipart;
                    if (this.fpart + obj.fpart > 99)
                    {
                        this.ipart++;
                        this.fpart = Convert.ToByte(this.fpart + obj.fpart - 100);
                    }
                    else
                    {
                        this.fpart += obj.fpart;
                    }
                }
            }
            else
            {
                if (this.sign)    // a - -b (same as a + b)
                {
                    this.ipart += obj.ipart;
                    if (this.fpart + obj.fpart > 99)
                    {
                        this.ipart++;
                        this.fpart = Convert.ToByte(this.fpart + obj.fpart - 100);
                    }
                    else
                    {
                        this.fpart += obj.fpart;
                    }
                }
                else    // -a - -b (same as -a + b)
                {
                    if (obj.ipart - this.ipart >= 0)    // -a + b >= 0
                    {
                        this.sign = true;
                        this.ipart = -this.ipart + obj.ipart;
                        if (this.fpart <= obj.fpart)
                        {
                            this.fpart = Convert.ToByte(obj.fpart - this.fpart);
                        }
                        else
                        {
                            this.ipart--;
                            this.fpart = Convert.ToByte(100 - (obj.fpart - this.fpart));
                        }
                    }
                    else    // -a + b < 0
                    {
                        this.ipart = -(-this.ipart + obj.ipart);
                        if (this.fpart < obj.fpart)
                        {
                            this.ipart--;
                            this.fpart = Convert.ToByte(100 - (obj.fpart - this.fpart));
                        }
                        else
                        {
                            this.fpart = Convert.ToByte(obj.fpart - this.fpart);
                        }
                    }
                }
            }
        }

        public bool Equals(Money other)    // #10
        {
            return this.sign.Equals(other.sign) & this.ipart.Equals(other.ipart) & this.fpart.Equals(other.fpart);
        }

        public int CompareTo(object obj)    // #11
        {
            Money other = (Money)(obj);
            if ((Convert.ToDouble(this.ToString()) > Convert.ToDouble(other.ToString())))
            {
                return 1;
            }
            else if ((Convert.ToDouble(this.ToString()) < Convert.ToDouble(other.ToString())))
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        public static Money MSum(Money obj_1, Money obj_2)    // #12
        {
            Money obj_3 = new Money(obj_1);
            obj_3.MAdd2(obj_2);
            return obj_3;
        }

        public static Money operator +(Money obj_1, Money obj_2)    // #12 (by operator overloading)
        {
            Money obj_3 = new Money(obj_1);
            obj_3.MAdd2(obj_2);
            return obj_3;
        }

        public static Money MDif(Money obj_1, Money obj_2)    // #13
        {
            Money obj_3 = new Money(obj_1);
            obj_3.MSub2(obj_2);
            return obj_3;
        }

        public static Money operator -(Money obj_1, Money obj_2)    // #13 (by operator - overloading)
        {
            Money obj_3 = new Money(obj_1);
            obj_3.MSub2(obj_2);
            return obj_3;
        }

        public Money MMul(float value) // #14
        {
            float a = float.Parse(this.ToString());
            a *= value;
            a = (float)Math.Round(Convert.ToDouble(a), 2);
            Money result = new Money(Convert.ToString(a));
            return result;
        }

        public Money MDiv1(float value) // #15
        {
            float a = float.Parse(this.ToString());
            a /= value;
            a = (float)Math.Round(Convert.ToDouble(a), 2);
            Money result = new Money(Convert.ToString(a));
            return result;
        }

        public float MDiv2(Money obj)    // #16
        {
            float temp = float.Parse(this.ToString());
            float temp_1 = float.Parse(obj.ToString());
            float result = temp / temp_1;
            return result;
        }
    }
}
