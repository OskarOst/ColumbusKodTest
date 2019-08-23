using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColumbusKodTest
{
    class MergeSort
    {
        public List<Card> Sort(List<Card> input)
        {
            if (input.Count == 1)
            {
                //Om det bara är ett objekt i listan returnera det för den är redan delad så långt det går
                return input;
            }
            //två listor skapas, en vänster och en höger för att representera två högar där korten läggs. 
            List<Card> left = new List<Card>();
            List<Card> right = new List<Card>();
            //mitten räknas ut. 
            int middle = input.Count / 2;

            //1/2 av alla objekt läggs till i left listan. 
            for (int i = 0; i < middle; i++)
            {
                left.Add(input[i]);
            }
            //Resten läggs till i höger listan. 
            for (int i = middle; i < input.Count; i++)
            {
                right.Add(input[i]);
            }
            //Samma sak upprepas med höger och vänster listan separat för att dela upp dem mer.
            left = Sort(left);
            right = Sort(right);
            //Båda listorna läggs ihop och sorteras. 
            return Merge(left, right);
        }
        private List<Card> Merge(List<Card> left, List<Card> right)
        {
            //en temporär lista skapas. 
            List<Card> output = new List<Card>();
            // En while loop rullar medans det finns objekt i antingen i left eller höger listan. 
            while (left.Count > 0 || right.Count > 0)
            {
                //Om båda listorna har objekt i dem jämförs de båda listorna. 
                if (left.Count > 0 && right.Count > 0)
                {
                    if (left[0].CardValue() == right[0].CardValue())
                    {
                        //Har korten samma värde så jämförs deras färger. Är den till vänster värd mer läggs den till först
                        //följd av den till höger och båda tas bort från left och höger listan. 
                        if (left[0].Type() < right[0].Type())
                        {
                            output.Add(left[0]);
                            output.Add(right[0]);
                            left.RemoveAt(0);
                            right.RemoveAt(0);
                        }
                        else
                        {
                            output.Add(right[0]);
                            output.Add(left[0]);
                            left.RemoveAt(0);
                            right.RemoveAt(0);
                        }
                    }
                    else if (left[0].CardValue() < right[0].CardValue())
                    {
                        //Är det vänstra värdet lägre än det högra läggs det till och tas bort från den vänstra högen. 
                        output.Add(left[0]);
                        left.RemoveAt(0);
                    }
                    else
                    {
                        //Annars läggs det högra värdet till och tas bort från den högra högen. 
                        output.Add(right[0]);
                        right.RemoveAt(0);
                    }

                }

                //Men om bara vänstra listan har objekt i sig jämförs den istället med den sista positionen i den temporära listan. 
                else if (left.Count > 0)
                {
                    if (output[output.Count - 1].CardValue() == left[0].CardValue())
                    {
                        //Har korten samma nummer så jämförs deras färger, jämför med ovan men höger ersatt med temp listan.
                        if (output[output.Count - 1].Type() < left[0].Type())
                        {
                            output.Add(left[0]);
                        }
                        else
                        {
                            output.Add(output[output.Count - 1]);
                            output[output.Count - 2] = left[0];
                        }
                    }
                    else if (output[output.Count - 1].CardValue() < left[0].CardValue())
                    {
                        output.Add(left[0]);
                    }
                    else
                    {
                        output.Add(output[output.Count - 1]);
                        output[output.Count - 2] = left[0];
                    }
                    left.RemoveAt(0);
                }
                //Men om bara högra listan har objekt i sig jämförs den istället med den sista positionen i den temporära listan. 
                else if (right.Count > 0)
                {
                    //Har korten samma nummer så jämförs deras färger, jämför med ovan men vänster ersatt med temp listan.
                    if (output[output.Count - 1].CardValue() == right[0].CardValue())
                    {
                        if (output[output.Count - 1].Type() < right[0].Type())
                        {
                            output.Add(right[0]);
                        }
                        else
                        {
                            output.Add(output[output.Count - 1]);
                            output[output.Count - 2] = right[0];
                        }
                    }
                    else if (output[output.Count - 1].CardValue() < right[0].CardValue())
                    {
                        output.Add(right[0]);
                    }
                    else
                    {
                        output.Add(output[output.Count - 1]);
                        output[output.Count - 2] = right[0];
                    }
                    right.RemoveAt(0);
                }
            }
            return output;
        }
    }
}
