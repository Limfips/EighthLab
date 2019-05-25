using System.Collections.Generic;

namespace WpfProject.FirstTask
{
    public class FirstTask
    {
        public List<List<int>> GetValues(int n, int m)
        {
            List<List<int>> values = new List<List<int>>(n);

            for (int i = 0, k = 0; i < n; i++, k++)
            {
                values.Add(new List<int>(m));

                for (int j = 0, q = 0; j < m; j++, q++)
                {
                    values[i].Add(q*k);
                }
            }
            return values;
        }
    }
}