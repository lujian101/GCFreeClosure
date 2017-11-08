using System;
using System.Collections.Generic;
using Common;

namespace FGDKit.Base {

    // Memory friendly multi-cast-delegate implementation
    // It could eliminate unnecessary GC-allocation for delegate cloning( add, remove )
    // Usage:
    // class Test {
    //    BetterEvent<int> callback;
    //    Test() {
    //        callback += Func;
    //        if ( callback ) {
    //            callback.Invoke( 0 );
    //        }
    //    }
    //    void Func( int a ) {
    //        // no problem at all
    //        callback -= Func;
    //    }
    //}

    public struct BetterEvent {

        List<Action> m_calleeList;
        int m_depth;
        int m_sparseIndex;

        public static implicit operator bool( BetterEvent exists ) {
            return exists.m_calleeList != null && exists.m_calleeList.Count > 0;
        }

        public static BetterEvent operator +( BetterEvent lhs, Action rhs ) {
            lhs.slot += rhs;
            return lhs;
        }

        public static BetterEvent operator -( BetterEvent lhs, Action rhs ) {
            lhs.slot -= rhs;
            return lhs;
        }

        public event Action slot {
            add {
                if ( value != null ) {
                    if ( m_calleeList == null ) {
                        m_calleeList = new List<Action>( 1 );
                    }
                    m_calleeList.Add( value );
                }
            }
            remove {
                if ( value != null ) {
                    var _list = m_calleeList;
                    if ( _list != null ) {
                        if ( m_depth != 0 ) {
                            for ( int i = 0, count = _list.Count; i < count; ++i ) {
                                if ( _list[ i ] != null && _list[ i ].Equals( value ) ) {
                                    _list[ i ] = null;
                                    if ( i <= m_sparseIndex ) {
                                        m_sparseIndex = i + 1;
                                    }
                                }
                            }
                        } else {
                            _list.Remove( value );
                        }
                    }
                }
            }
        }

        public void Invoke() {
            var list_ = m_calleeList;
            if ( list_ != null ) {
                try {
                    ++m_depth;
                    for ( int i = 0; i < list_.Count; ++i ) {
                        if ( list_[ i ] != null ) {
                            try {
                                list_[ i ]();
                            } catch ( Exception e ) {
                                UDebug.LogException( e );
                            }
                        }
                    }
                } finally {
                    --m_depth;
                    if ( m_sparseIndex > 0 && m_depth == 0 && list_.Count > 0 ) {
                        var count = list_.Count;
                        var removeCount = 0;
                        for ( var i = m_sparseIndex - 1; i < count; ++i ) {
                            if ( list_[ i ] == null ) {
                                var newCount = i++;
                                for ( ; i < count; ++i ) {
                                    if ( list_[ i ] != null ) {
                                        list_[ newCount++ ] = list_[ i ];
                                    }
                                }
                                removeCount = count - newCount;
                                list_.RemoveRange( newCount, removeCount );
                                break;
                            }
                        }
                        m_sparseIndex = 0;
                    }
                }
            }
        }
    }

    public struct BetterEvent<T> {

        List<Action<T>> m_calleeList;
        int m_depth;
        int m_sparseIndex;

        public static BetterEvent<T> operator +( BetterEvent<T> lhs, Action<T> rhs ) {
            lhs.slot += rhs;
            return lhs;
        }

        public static BetterEvent<T> operator -( BetterEvent<T> lhs, Action<T> rhs ) {
            lhs.slot -= rhs;
            return lhs;
        }

        public static implicit operator bool( BetterEvent<T> exists ) {
            return exists.m_calleeList != null && exists.m_calleeList.Count > 0;
        }

        public event Action<T> slot {
            add {
                if ( value != null ) {
                    if ( m_calleeList == null ) {
                        m_calleeList = new List<Action<T>>( 1 );
                    }
                    m_calleeList.Add( value );
                }
            }
            remove {
                if ( value != null ) {
                    var _list = m_calleeList;
                    if ( _list != null ) {
                        if ( m_depth != 0 ) {
                            for ( int i = 0, count = _list.Count; i < count; ++i ) {
                                if ( _list[ i ] != null && _list[ i ].Equals( value ) ) {
                                    _list[ i ] = null;
                                    if ( i <= m_sparseIndex ) {
                                        m_sparseIndex = i + 1;
                                    }
                                }
                            }
                        } else {
                            _list.Remove( value );
                        }
                    }
                }
            }
        }

        public void Invoke( T arg ) {
            var list_ = m_calleeList;
            if ( list_ != null ) {
                try {
                    ++m_depth;
                    for ( int i = 0; i < list_.Count; ++i ) {
                        if ( list_[ i ] != null ) {
                            try {
                                list_[ i ]( arg );
                            } catch ( Exception e ) {
                                UDebug.LogException( e );
                            }
                        }
                    }
                } finally {
                    --m_depth;
                    if ( m_sparseIndex > 0 && m_depth == 0 && list_.Count > 0 ) {
                        var count = list_.Count;
                        var removeCount = 0;
                        for ( var i = m_sparseIndex - 1; i < count; ++i ) {
                            if ( list_[ i ] == null ) {
                                var newCount = i++;
                                for ( ; i < count; ++i ) {
                                    if ( list_[ i ] != null ) {
                                        list_[ newCount++ ] = list_[ i ];
                                    }
                                }
                                removeCount = count - newCount;
                                list_.RemoveRange( newCount, removeCount );
                                break;
                            }
                        }
                        m_sparseIndex = 0;
                    }
                }
            }
        }
    }

    public struct BetterEvent<T1, T2> {

        List<Action<T1, T2>> m_calleeList;
        int m_depth;
        int m_sparseIndex;

        public static BetterEvent<T1, T2> operator +( BetterEvent<T1, T2> lhs, Action<T1, T2> rhs ) {
            lhs.slot += rhs;
            return lhs;
        }

        public static BetterEvent<T1, T2> operator -( BetterEvent<T1, T2> lhs, Action<T1, T2> rhs ) {
            lhs.slot -= rhs;
            return lhs;
        }

        public static implicit operator bool( BetterEvent<T1, T2> exists ) {
            return exists.m_calleeList != null && exists.m_calleeList.Count > 0;
        }

        public event Action<T1, T2> slot {
            add {
                if ( value != null ) {
                    if ( m_calleeList == null ) {
                        m_calleeList = new List<Action<T1, T2>>( 1 );
                    }
                    m_calleeList.Add( value );
                }
            }
            remove {
                if ( value != null ) {
                    var _list = m_calleeList;
                    if ( _list != null ) {
                        if ( m_depth != 0 ) {
                            for ( int i = 0, count = _list.Count; i < count; ++i ) {
                                if ( _list[ i ] != null && _list[ i ].Equals( value ) ) {
                                    _list[ i ] = null;
                                    if ( i <= m_sparseIndex ) {
                                        m_sparseIndex = i + 1;
                                    }
                                }
                            }
                        } else {
                            _list.Remove( value );
                        }
                    }
                }
            }
        }

        public void Invoke( T1 arg1, T2 arg2 ) {
            var list_ = m_calleeList;
            if ( list_ != null ) {
                try {
                    ++m_depth;
                    for ( int i = 0; i < list_.Count; ++i ) {
                        if ( list_[ i ] != null ) {
                            try {
                                list_[ i ]( arg1, arg2 );
                            } catch ( Exception e ) {
                                UDebug.LogException( e );
                            }
                        }
                    }
                } finally {
                    --m_depth;
                    if ( m_sparseIndex > 0 && m_depth == 0 && list_.Count > 0 ) {
                        var count = list_.Count;
                        var removeCount = 0;
                        for ( var i = m_sparseIndex - 1; i < count; ++i ) {
                            if ( list_[ i ] == null ) {
                                var newCount = i++;
                                for ( ; i < count; ++i ) {
                                    if ( list_[ i ] != null ) {
                                        list_[ newCount++ ] = list_[ i ];
                                    }
                                }
                                removeCount = count - newCount;
                                list_.RemoveRange( newCount, removeCount );
                                break;
                            }
                        }
                        m_sparseIndex = 0;
                    }
                }
            }
        }
    }

    public struct BetterEvent<T1, T2, T3> {

        List<Action<T1, T2, T3>> m_calleeList;
        int m_depth;
        int m_sparseIndex;

        public static BetterEvent<T1, T2, T3> operator +( BetterEvent<T1, T2, T3> lhs, Action<T1, T2, T3> rhs ) {
            lhs.slot += rhs;
            return lhs;
        }

        public static BetterEvent<T1, T2, T3> operator -( BetterEvent<T1, T2, T3> lhs, Action<T1, T2, T3> rhs ) {
            lhs.slot -= rhs;
            return lhs;
        }

        public static implicit operator bool( BetterEvent<T1, T2, T3> exists ) {
            return exists.m_calleeList != null && exists.m_calleeList.Count > 0;
        }

        public event Action<T1, T2, T3> slot {
            add {
                if ( value != null ) {
                    if ( m_calleeList == null ) {
                        m_calleeList = new List<Action<T1, T2, T3>>( 1 );
                    }
                    m_calleeList.Add( value );
                }
            }
            remove {
                if ( value != null ) {
                    var _list = m_calleeList;
                    if ( _list != null ) {
                        if ( m_depth != 0 ) {
                            for ( int i = 0, count = _list.Count; i < count; ++i ) {
                                if ( _list[ i ] != null && _list[ i ].Equals( value ) ) {
                                    _list[ i ] = null;
                                    if ( i <= m_sparseIndex ) {
                                        m_sparseIndex = i + 1;
                                    }
                                }
                            }
                        } else {
                            _list.Remove( value );
                        }
                    }
                }
            }
        }

        public void Invoke( T1 arg1, T2 arg2, T3 arg3 ) {
            var list_ = m_calleeList;
            if ( list_ != null ) {
                try {
                    ++m_depth;
                    for ( int i = 0; i < list_.Count; ++i ) {
                        if ( list_[ i ] != null ) {
                            try {
                                list_[ i ]( arg1, arg2, arg3 );
                            } catch ( Exception e ) {
                                UDebug.LogException( e );
                            }
                        }
                    }
                } finally {
                    --m_depth;
                    if ( m_sparseIndex > 0 && m_depth == 0 && list_.Count > 0 ) {
                        var count = list_.Count;
                        var removeCount = 0;
                        for ( var i = m_sparseIndex - 1; i < count; ++i ) {
                            if ( list_[ i ] == null ) {
                                var newCount = i++;
                                for ( ; i < count; ++i ) {
                                    if ( list_[ i ] != null ) {
                                        list_[ newCount++ ] = list_[ i ];
                                    }
                                }
                                removeCount = count - newCount;
                                list_.RemoveRange( newCount, removeCount );
                                break;
                            }
                        }
                        m_sparseIndex = 0;
                    }
                }
            }
        }
    }

    public struct BetterEvent<T1, T2, T3, T4> {

        List<Action<T1, T2, T3, T4>> m_calleeList;
        int m_depth;
        int m_sparseIndex;

        public static BetterEvent<T1, T2, T3, T4> operator +( BetterEvent<T1, T2, T3, T4> lhs, Action<T1, T2, T3, T4> rhs ) {
            lhs.slot += rhs;
            return lhs;
        }

        public static BetterEvent<T1, T2, T3, T4> operator -( BetterEvent<T1, T2, T3, T4> lhs, Action<T1, T2, T3, T4> rhs ) {
            lhs.slot -= rhs;
            return lhs;
        }

        public static implicit operator bool( BetterEvent<T1, T2, T3, T4> exists ) {
            return exists.m_calleeList != null && exists.m_calleeList.Count > 0;
        }

        public event Action<T1, T2, T3, T4> slot {
            add {
                if ( value != null ) {
                    if ( m_calleeList == null ) {
                        m_calleeList = new List<Action<T1, T2, T3, T4>>( 1 );
                    }
                    m_calleeList.Add( value );
                }
            }
            remove {
                if ( value != null ) {
                    var _list = m_calleeList;
                    if ( _list != null ) {
                        if ( m_depth != 0 ) {
                            for ( int i = 0, count = _list.Count; i < count; ++i ) {
                                if ( _list[ i ] != null && _list[ i ].Equals( value ) ) {
                                    _list[ i ] = null;
                                    if ( i <= m_sparseIndex ) {
                                        m_sparseIndex = i + 1;
                                    }
                                }
                            }
                        } else {
                            _list.Remove( value );
                        }
                    }
                }
            }
        }

        public void Invoke( T1 arg1, T2 arg2, T3 arg3, T4 arg4 ) {
            var list_ = m_calleeList;
            if ( list_ != null ) {
                try {
                    ++m_depth;
                    for ( int i = 0; i < list_.Count; ++i ) {
                        if ( list_[ i ] != null ) {
                            try {
                                list_[ i ]( arg1, arg2, arg3, arg4 );
                            } catch ( Exception e ) {
                                UDebug.LogException( e );
                            }
                        }
                    }
                } finally {
                    --m_depth;
                    if ( m_sparseIndex > 0 && m_depth == 0 && list_.Count > 0 ) {
                        var count = list_.Count;
                        var removeCount = 0;
                        for ( var i = m_sparseIndex - 1; i < count; ++i ) {
                            if ( list_[ i ] == null ) {
                                var newCount = i++;
                                for ( ; i < count; ++i ) {
                                    if ( list_[ i ] != null ) {
                                        list_[ newCount++ ] = list_[ i ];
                                    }
                                }
                                removeCount = count - newCount;
                                list_.RemoveRange( newCount, removeCount );
                                break;
                            }
                        }
                        m_sparseIndex = 0;
                    }
                }
            }
        }
    }
}
//EOF
