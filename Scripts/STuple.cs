using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace System {

    internal interface ISTuple {
        string ToString( StringBuilder sb );
        int Size { get; }
    }

    public static class STuple {

        public static STuple<T1> Create<T1>( T1 item1 ) {
            return new STuple<T1>( item1 );
        }

        public static STuple<T1, T2> Create<T1, T2>( T1 item1, T2 item2 ) {
            return new STuple<T1, T2>( item1, item2 );
        }

        public static STuple<T1, T2, T3> Create<T1, T2, T3>( T1 item1, T2 item2, T3 item3 ) {
            return new STuple<T1, T2, T3>( item1, item2, item3 );
        }

        public static STuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>( T1 item1, T2 item2, T3 item3, T4 item4 ) {
            return new STuple<T1, T2, T3, T4>( item1, item2, item3, item4 );
        }

        public static STuple<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>( T1 item1, T2 item2, T3 item3, T4 item4, T5 item5 ) {
            return new STuple<T1, T2, T3, T4, T5>( item1, item2, item3, item4, item5 );
        }

        public static STuple<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>( T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6 ) {
            return new STuple<T1, T2, T3, T4, T5, T6>( item1, item2, item3, item4, item5, item6 );
        }

        public static STuple<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>( T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7 ) {
            return new STuple<T1, T2, T3, T4, T5, T6, T7>( item1, item2, item3, item4, item5, item6, item7 );
        }

        public static STuple<T1, T2, T3, T4, T5, T6, T7, STuple<T8>> Create<T1, T2, T3, T4, T5, T6, T7, T8>( T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8 ) {
            return new STuple<T1, T2, T3, T4, T5, T6, T7, STuple<T8>>( item1, item2, item3, item4, item5, item6, item7, new STuple<T8>( item8 ) );
        }
    }

    public struct STuple<T1> : ISTuple {

        private readonly T1 m_Item1;

        public T1 Item1 { get { return m_Item1; } }

        public STuple( T1 item1 ) {
            m_Item1 = item1;
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append( "(" );
            return ( ( ISTuple )this ).ToString( sb );
        }

        string ISTuple.ToString( StringBuilder sb ) {
            sb.Append( m_Item1 );
            sb.Append( ")" );
            return sb.ToString();
        }

        int ISTuple.Size {
            get {
                return 1;
            }
        }
    }

    public struct STuple<T1, T2> : ISTuple {

        private readonly T1 m_Item1;
        private readonly T2 m_Item2;

        public T1 Item1 { get { return m_Item1; } }
        public T2 Item2 { get { return m_Item2; } }

        public STuple( T1 item1, T2 item2 ) {
            m_Item1 = item1;
            m_Item2 = item2;
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append( "(" );
            return ( ( ISTuple )this ).ToString( sb );
        }

        string ISTuple.ToString( StringBuilder sb ) {
            sb.Append( m_Item1 );
            sb.Append( ", " );
            sb.Append( m_Item2 );
            sb.Append( ")" );
            return sb.ToString();
        }

        int ISTuple.Size {
            get {
                return 2;
            }
        }
    }

    public struct STuple<T1, T2, T3> : ISTuple {

        private readonly T1 m_Item1;
        private readonly T2 m_Item2;
        private readonly T3 m_Item3;

        public T1 Item1 { get { return m_Item1; } }
        public T2 Item2 { get { return m_Item2; } }
        public T3 Item3 { get { return m_Item3; } }

        public STuple( T1 item1, T2 item2, T3 item3 ) {
            m_Item1 = item1;
            m_Item2 = item2;
            m_Item3 = item3;
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append( "(" );
            return ( ( ISTuple )this ).ToString( sb );
        }

        string ISTuple.ToString( StringBuilder sb ) {
            sb.Append( m_Item1 );
            sb.Append( ", " );
            sb.Append( m_Item2 );
            sb.Append( ", " );
            sb.Append( m_Item3 );
            sb.Append( ")" );
            return sb.ToString();
        }

        int ISTuple.Size {
            get {
                return 3;
            }
        }
    }

    public struct STuple<T1, T2, T3, T4> : ISTuple {

        private readonly T1 m_Item1;
        private readonly T2 m_Item2;
        private readonly T3 m_Item3;
        private readonly T4 m_Item4;

        public T1 Item1 { get { return m_Item1; } }
        public T2 Item2 { get { return m_Item2; } }
        public T3 Item3 { get { return m_Item3; } }
        public T4 Item4 { get { return m_Item4; } }

        public STuple( T1 item1, T2 item2, T3 item3, T4 item4 ) {
            m_Item1 = item1;
            m_Item2 = item2;
            m_Item3 = item3;
            m_Item4 = item4;
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append( "(" );
            return ( ( ISTuple )this ).ToString( sb );
        }

        string ISTuple.ToString( StringBuilder sb ) {
            sb.Append( m_Item1 );
            sb.Append( ", " );
            sb.Append( m_Item2 );
            sb.Append( ", " );
            sb.Append( m_Item3 );
            sb.Append( ", " );
            sb.Append( m_Item4 );
            sb.Append( ")" );
            return sb.ToString();
        }

        int ISTuple.Size {
            get {
                return 4;
            }
        }
    }

    public struct STuple<T1, T2, T3, T4, T5> : ISTuple {

        private readonly T1 m_Item1;
        private readonly T2 m_Item2;
        private readonly T3 m_Item3;
        private readonly T4 m_Item4;
        private readonly T5 m_Item5;

        public T1 Item1 { get { return m_Item1; } }
        public T2 Item2 { get { return m_Item2; } }
        public T3 Item3 { get { return m_Item3; } }
        public T4 Item4 { get { return m_Item4; } }
        public T5 Item5 { get { return m_Item5; } }

        public STuple( T1 item1, T2 item2, T3 item3, T4 item4, T5 item5 ) {
            m_Item1 = item1;
            m_Item2 = item2;
            m_Item3 = item3;
            m_Item4 = item4;
            m_Item5 = item5;
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append( "(" );
            return ( ( ISTuple )this ).ToString( sb );
        }

        string ISTuple.ToString( StringBuilder sb ) {
            sb.Append( m_Item1 );
            sb.Append( ", " );
            sb.Append( m_Item2 );
            sb.Append( ", " );
            sb.Append( m_Item3 );
            sb.Append( ", " );
            sb.Append( m_Item4 );
            sb.Append( ", " );
            sb.Append( m_Item5 );
            sb.Append( ")" );
            return sb.ToString();
        }

        int ISTuple.Size {
            get {
                return 5;
            }
        }
    }

    public struct STuple<T1, T2, T3, T4, T5, T6> : ISTuple {

        private readonly T1 m_Item1;
        private readonly T2 m_Item2;
        private readonly T3 m_Item3;
        private readonly T4 m_Item4;
        private readonly T5 m_Item5;
        private readonly T6 m_Item6;

        public T1 Item1 { get { return m_Item1; } }
        public T2 Item2 { get { return m_Item2; } }
        public T3 Item3 { get { return m_Item3; } }
        public T4 Item4 { get { return m_Item4; } }
        public T5 Item5 { get { return m_Item5; } }
        public T6 Item6 { get { return m_Item6; } }

        public STuple( T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6 ) {
            m_Item1 = item1;
            m_Item2 = item2;
            m_Item3 = item3;
            m_Item4 = item4;
            m_Item5 = item5;
            m_Item6 = item6;
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append( "(" );
            return ( ( ISTuple )this ).ToString( sb );
        }

        string ISTuple.ToString( StringBuilder sb ) {
            sb.Append( m_Item1 );
            sb.Append( ", " );
            sb.Append( m_Item2 );
            sb.Append( ", " );
            sb.Append( m_Item3 );
            sb.Append( ", " );
            sb.Append( m_Item4 );
            sb.Append( ", " );
            sb.Append( m_Item5 );
            sb.Append( ", " );
            sb.Append( m_Item6 );
            sb.Append( ")" );
            return sb.ToString();
        }

        int ISTuple.Size {
            get {
                return 6;
            }
        }
    }

    public struct STuple<T1, T2, T3, T4, T5, T6, T7> : ISTuple {

        private readonly T1 m_Item1;
        private readonly T2 m_Item2;
        private readonly T3 m_Item3;
        private readonly T4 m_Item4;
        private readonly T5 m_Item5;
        private readonly T6 m_Item6;
        private readonly T7 m_Item7;

        public T1 Item1 { get { return m_Item1; } }
        public T2 Item2 { get { return m_Item2; } }
        public T3 Item3 { get { return m_Item3; } }
        public T4 Item4 { get { return m_Item4; } }
        public T5 Item5 { get { return m_Item5; } }
        public T6 Item6 { get { return m_Item6; } }
        public T7 Item7 { get { return m_Item7; } }

        public STuple( T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7 ) {
            m_Item1 = item1;
            m_Item2 = item2;
            m_Item3 = item3;
            m_Item4 = item4;
            m_Item5 = item5;
            m_Item6 = item6;
            m_Item7 = item7;
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append( "(" );
            return ( ( ISTuple )this ).ToString( sb );
        }

        string ISTuple.ToString( StringBuilder sb ) {
            sb.Append( m_Item1 );
            sb.Append( ", " );
            sb.Append( m_Item2 );
            sb.Append( ", " );
            sb.Append( m_Item3 );
            sb.Append( ", " );
            sb.Append( m_Item4 );
            sb.Append( ", " );
            sb.Append( m_Item5 );
            sb.Append( ", " );
            sb.Append( m_Item6 );
            sb.Append( ", " );
            sb.Append( m_Item7 );
            sb.Append( ")" );
            return sb.ToString();
        }

        int ISTuple.Size {
            get {
                return 7;
            }
        }
    }

    public struct STuple<T1, T2, T3, T4, T5, T6, T7, TRest> : ISTuple {

        private readonly T1 m_Item1;
        private readonly T2 m_Item2;
        private readonly T3 m_Item3;
        private readonly T4 m_Item4;
        private readonly T5 m_Item5;
        private readonly T6 m_Item6;
        private readonly T7 m_Item7;
        private readonly TRest m_Rest;

        public T1 Item1 { get { return m_Item1; } }
        public T2 Item2 { get { return m_Item2; } }
        public T3 Item3 { get { return m_Item3; } }
        public T4 Item4 { get { return m_Item4; } }
        public T5 Item5 { get { return m_Item5; } }
        public T6 Item6 { get { return m_Item6; } }
        public T7 Item7 { get { return m_Item7; } }
        public TRest Rest { get { return m_Rest; } }

        public STuple( T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, TRest rest ) {
            m_Item1 = item1;
            m_Item2 = item2;
            m_Item3 = item3;
            m_Item4 = item4;
            m_Item5 = item5;
            m_Item6 = item6;
            m_Item7 = item7;
            m_Rest = rest;
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append( "(" );
            return ( ( ISTuple )this ).ToString( sb );
        }

        string ISTuple.ToString( StringBuilder sb ) {
            sb.Append( m_Item1 );
            sb.Append( ", " );
            sb.Append( m_Item2 );
            sb.Append( ", " );
            sb.Append( m_Item3 );
            sb.Append( ", " );
            sb.Append( m_Item4 );
            sb.Append( ", " );
            sb.Append( m_Item5 );
            sb.Append( ", " );
            sb.Append( m_Item6 );
            sb.Append( ", " );
            sb.Append( m_Item7 );
            sb.Append( ", " );
            return ( ( ISTuple )m_Rest ).ToString( sb );
        }

        int ISTuple.Size {
            get {
                return 7 + ( ( ISTuple )m_Rest ).Size;
            }
        }
    }
}