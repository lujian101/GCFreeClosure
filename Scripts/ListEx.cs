using System;
using System.Collections.Generic;

namespace FGDKit.Base {

    public static class ListEx {

        public static void Resize<T>( this List<T> list, int sz, T c ) {
            int cur = list.Count;
            if ( sz < cur ) {
                list.RemoveRange( sz, cur - sz );
            } else if ( sz > cur ) {
                //this bit is purely an optimisation, to avoid multiple automatic capacity changes.
                if ( sz > list.Capacity ) {
                    list.Capacity = sz;
                }
                int count = sz - cur;
                for ( int i = 0; i < count; ++i ) {
                    list.Add( c );
                }
            }
        }

        public static void Resize<T>( this List<T> list, int sz ) {
            Resize( list, sz, default( T ) );
        }

        public static int FindIndex<T, C>( this IList<T> list, C ctx, Func<T, C, bool> match ) {
            UDebug.Assert( ( Delegate )match.Target == null );
            for ( int i = 0, count = list.Count; i < count; ++i ) {
                if ( match( list[ i ], ctx ) ) {
                    return i;
                }
            }
            return -1;
        }

        public static int FindLastIndex<T, C>( this IList<T> list, C ctx, Func<T, C, bool> match ) {
            UDebug.Assert( ( Delegate )match.Target == null );
            for ( int i = list.Count - 1; i >= 0; --i ) {
                if ( match( list[ i ], ctx ) ) {
                    return i;
                }
            }
            return -1;
        }

        public static bool RemoveFirstOf<T, C>( this IList<T> list, C ctx, Func<T, C, bool> match ) {
            var index = list.FindIndex( ctx, match );
            if ( index != -1 ) {
                list.RemoveAt( index );
                return true;
            }
            return false;
        }

        public static bool RemoveLastOf<T, C>( this IList<T> list, C ctx, Func<T, C, bool> match ) {
            var index = list.FindLastIndex( ctx, match );
            if ( index != -1 ) {
                list.RemoveAt( index );
                return true;
            }
            return false;
        }

        public static int RemoveAllNull<T>( this List<T> list ) where T : class {
            var count = list.Count;
            var removeCount = 0;
            for ( var i = 0; i < count; ++i ) {
                if ( list[ i ] == null ) {
                    var newCount = i++;
                    for ( ; i < count; ++i ) {
                        if ( list[ i ] != null ) {
                            list[ newCount++ ] = list[ i ];
                        }
                    }
                    removeCount = count - newCount;
                    list.RemoveRange( newCount, removeCount );
                    break;
                }
            }
            return removeCount;
        }

        public static int RemoveAllNullUnordered<T>( this List<T> list ) where T : class {
            var count = list.Count;
            var last = count - 1;
            var removeCount = 0;
            for ( var i = 0; i <= last; ) {
                if ( list[ i ] == null ) {
                    if ( last != i ) {
                        list[ i ] = list[ last ];
                    }
                    --last;
                    ++removeCount;
                } else {
                    ++i;
                }
            }
            if ( removeCount > 0 ) {
                list.RemoveRange( count - removeCount, removeCount );
            }
            return removeCount;
        }

        public static int RemoveAll<T, C>( this List<T> list, C ctx, Func<T, C, bool> match ) {
            UDebug.Assert( ( Delegate )match.Target == null );
            var count = list.Count;
            var removeCount = 0;
            for ( var i = 0; i < count; ++i ) {
                if ( match( list[ i ], ctx ) ) {
                    var newCount = i++;
                    for ( ; i < count; ++i ) {
                        if ( !match( list[ i ], ctx ) ) {
                            list[ newCount++ ] = list[ i ];
                        }
                    }
                    removeCount = count - newCount;
                    list.RemoveRange( newCount, removeCount );
                    break;
                }
            }
            return removeCount;
        }

        public static int RemoveAllUnordered<T, C>( this List<T> list, C ctx, Func<T, C, bool> match ) {
            UDebug.Assert( ( Delegate )match.Target == null );
            var count = list.Count;
            var last = count - 1;
            var removeCount = 0;
            for ( var i = 0; i <= last; ) {
                if ( match( list[ i ], ctx ) ) {
                    if ( last != i ) {
                        list[ i ] = list[ last ];
                    }
                    --last;
                    ++removeCount;
                } else {
                    ++i;
                }
            }
            if ( removeCount > 0 ) {
                list.RemoveRange( count - removeCount, removeCount );
            }
            return removeCount;
        }
    }
}
//EOF
