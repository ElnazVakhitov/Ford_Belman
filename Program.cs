using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ford_Belman
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("in.txt");
            args = sr.ReadToEnd().Split('\n');
            var n = int.Parse(args[0]);
            var m = Enumerable
                .Range(1, n)
                .Select(x => args[x])
                .Select(x => x.Split(' ').Select(y => long.Parse(y)).ToArray())
                .ToArray();

            var start = int.Parse(args[n + 1])-1;
            var end = int.Parse(args[n + 2])-1;
            var d = m[start].Select(x => x).ToArray();
            var prev = Enumerable.Repeat(start, n).ToArray();
            for (var k = 1; k < n; k++)
                for (var v = 0; v < n; v++)
                {
                    if (v == start)
                        continue;
                    for (var w = 0; w < n; w++)
                    {
                        if (w == start || v == w)
                            continue;
                        if (m[w][v] != -32768 && d[v] < m[w][v] * d[w])
                        {
                            d[v] = m[w][v] * d[w];
                            prev[v] = w;
                        }
                    }
                }
            StreamWriter f = new StreamWriter("out.txt");
            if (d[end] == -32768 && end != start)
                f.Write("N");
            else
            {
                f.WriteLine("Y");
                var result = new List<int>();
                var c = end;
                while(c != start)
                {
                    result.Add(c+1);
                    c = prev[c];
                }
                result.Add(start+1);
                result.Reverse();
                f.WriteLine(String.Join(" ", result));
            }
            f.Close();
        }
    }
}
