using System;
using System.Collections.Generic;
using System.Text;

namespace s1
{
    public class SampleBuilder
    {
        private struct Item
        {
            public double[] array;
            public long from, len;

            public Item(double[] array)
            {
                this.array = array;
                from = 0;
                len = array.LongLength;
            }

            public Item(double[] array, long from, long len)
            {
                this.array = array;
                this.from = from;
                this.len = len;
            }

            public void CopyTo(double[] dest, long destPos)
            {
                CopyTo(dest, destPos, this.len);
            }
            
            public void CopyTo(double[] dest, long destPos, long len)
            {
                Array.Copy(array, from, dest, destPos, len);
            }

            /// <summary>
            /// Gets a new Item, moving start pos relPos forward
            /// </summary>
            /// <param name="relPos"></param>
            /// <returns></returns>
            public Item GetPart(long relPos)
            {
                return new Item(array, from + relPos, len - relPos);
            }

        }

        private LinkedList<Item> samples = new LinkedList<Item>();
        private long count = 0;

        public long Count
        {
            get { return count; }
        }

        public void Add(double[] arr)
        {
            Add(arr, 0, arr.LongLength);
        }
        public void Add(double[] arr, long pos, long len)
        {
            samples.AddLast(new Item(arr, pos, len));
            count += len;
        }

        public void Clear()
        {
            samples.Clear();
            count = 0;
        }

        public double[] ToArray()
        {
            double[] res = new double[count];
            long pos = 0;
            foreach (Item itm in samples)
            {
                itm.CopyTo(res, pos);
                pos += itm.len;
            }
            return res;
        }

        /// <summary>
        /// Pops the enire build-up
        /// </summary>
        /// <returns></returns>
        public double[] Pop()
        {
            return Pop(Count);
        }

        /// <summary>
        /// Gets an array at most length long. Might be shorter, if too little data.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public double[] Pop(long length)
        {
            double[] res;

            if ( length >= count )
            {
                res = ToArray();
                Clear();
                return res;
            }

            res = new double[length];
            long pos =0;
            while ( length > 0 && samples.First != null )
            {
                Item first = samples.First.Value;
                if ( first.len <= length )
                {
                    // all of this sample
                    first.CopyTo(res, pos);
                    pos += samples.First.Value.len;
                    length -= samples.First.Value.len;                    
                }
                else 
                {
                    // parts of this sample...
                    first.CopyTo(res, pos, length);
                    samples.First.Value = first.GetPart(length);
                    pos += length;
                    length = 0;
                }
            }
            if ( pos < res.LongLength )
            {
                double[] old = res;
                res = new double[pos];
                Array.Copy(old, res, pos);
            }
            return res;
        }
    }
}
