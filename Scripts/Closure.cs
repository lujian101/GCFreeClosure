//#define CHECK_CLOSURE
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Common;

namespace FGDKit.Base {

    public struct Closure {

        public SValue _0;
        public SValue _1;
        public SValue _2;
        public SValue _3;
        public Delegate _delegate;

        public T ctx_0<T>() {
            return SValue.Reader<T>.invoke( ref _0 );
        }

        public T ctx_1<T>() {
            return SValue.Reader<T>.invoke( ref _1 );
        }

        public T ctx_2<T>() {
            return SValue.Reader<T>.invoke( ref _2 );
        }

        public T ctx_3<T>() {
            return SValue.Reader<T>.invoke( ref _3 );
        }

        public void Reset() {
            _delegate = null;
            _0 = SValue.nil;
            _1 = SValue.nil;
            _2 = SValue.nil;
            _3 = SValue.nil;
        }

        public void Invoke() {
            ( _delegate as Action )();
        }

        public void Invoke<T>() {
            ( _delegate as Action<T> )(
                SValue.Reader<T>.invoke( ref _0 )
            );
        }

        public void Invoke<T0, T1>() {
            ( _delegate as Action<T0, T1> )(
                SValue.Reader<T0>.invoke( ref _0 ),
                SValue.Reader<T1>.invoke( ref _1 )
            );
        }

        public void Invoke<T0, T1, T2>() {
            ( _delegate as Action<T0, T1, T2> )(
                SValue.Reader<T0>.invoke( ref _0 ),
                SValue.Reader<T1>.invoke( ref _1 ),
                SValue.Reader<T2>.invoke( ref _2 )
            );
        }

        public void Invoke<T0, T1, T2, T3>() {
            ( _delegate as Action<T0, T1, T2, T3> )(
                SValue.Reader<T0>.invoke( ref _0 ),
                SValue.Reader<T1>.invoke( ref _1 ),
                SValue.Reader<T2>.invoke( ref _2 ),
                SValue.Reader<T3>.invoke( ref _3 )
            );
        }

        public TResult RInvoke<TResult>() {
            return ( _delegate as Func<TResult> )();
        }

        public TResult RInvoke<T, TResult>() {
            return ( _delegate as Func<T, TResult> )(
                SValue.Reader<T>.invoke( ref _0 )
            );
        }

        public TResult RInvoke<T0, T1, TResult>() {
            return ( _delegate as Func<T0, T1, TResult> )(
                SValue.Reader<T0>.invoke( ref _0 ),
                SValue.Reader<T1>.invoke( ref _1 )
            );
        }

        public TResult RInvoke<T0, T1, T2, TResult>() {
            return ( _delegate as Func<T0, T1, T2, TResult> )(
                SValue.Reader<T0>.invoke( ref _0 ),
                SValue.Reader<T1>.invoke( ref _1 ),
                SValue.Reader<T2>.invoke( ref _2 )
            );
        }

        public TResult RInvoke<T0, T1, T2, T3, TResult>() {
            return ( _delegate as Func<T0, T1, T2, T3, TResult> )(
                SValue.Reader<T0>.invoke( ref _0 ),
                SValue.Reader<T1>.invoke( ref _1 ),
                SValue.Reader<T2>.invoke( ref _2 ),
                SValue.Reader<T3>.invoke( ref _3 )
            );
        }

        public SValue SRInvoke<TResult>() {
            return SValue.Writer<TResult>.invoke(
                ( _delegate as Func<TResult> )()
            );
        }

        public SValue SRInvoke<T, TResult>() {
            return SValue.Writer<TResult>.invoke(
                ( _delegate as Func<T, TResult> )(
                    SValue.Reader<T>.invoke( ref _0 )
                )
            );
        }

        public SValue SRInvoke<T0, T1, TResult>() {
            return SValue.Writer<TResult>.invoke(
                ( _delegate as Func<T0, T1, TResult> )(
                    SValue.Reader<T0>.invoke( ref _0 ),
                    SValue.Reader<T1>.invoke( ref _1 )
                )
            );
        }

        public SValue SRInvoke<T0, T1, T2, TResult>() {
            return SValue.Writer<TResult>.invoke(
                ( _delegate as Func<T0, T1, T2, TResult> )(
                    SValue.Reader<T0>.invoke( ref _0 ),
                    SValue.Reader<T1>.invoke( ref _1 ),
                    SValue.Reader<T2>.invoke( ref _2 )
                )
            );
        }

        public SValue SRInvoke<T0, T1, T2, T3, TResult>() {
            return SValue.Writer<TResult>.invoke(
                ( _delegate as Func<T0, T1, T2, T3, TResult> )(
                    SValue.Reader<T0>.invoke( ref _0 ),
                    SValue.Reader<T1>.invoke( ref _1 ),
                    SValue.Reader<T2>.invoke( ref _2 ),
                    SValue.Reader<T3>.invoke( ref _3 )
                )
            );
        }

        [Conditional( "CHECK_CLOSURE" )]
        public static void Check( object d ) {
            UDebug.Assert( ( ( Delegate )d ).Target == null );
        }
    }

    public struct ActionClosure {

        Closure _closure;
        Action<Closure> _wrapper;

        public void Reset() {
            _wrapper = null;
            _closure.Reset();
        }

        public void Invoke() {
            if ( _wrapper != null ) {
                _wrapper( _closure );
            }
        }

        public static ActionClosure Create( Action action ) {
            Closure.Check( action );
            return new ActionClosure {
                _closure = new Closure { _delegate = action },
                _wrapper = e => e.Invoke()
            };
        }

        public static ActionClosure Create<T>( Action<T> action, T ctx ) {
            Closure.Check( action );
            return new ActionClosure {
                _closure = new Closure {
                    _0 = SValue.Writer<T>.invoke( ctx ),
                    _delegate = action
                },
                _wrapper = ActionClosureWrapper<T>._default
            };
        }

        public static ActionClosure Create<T0, T1>( Action<T0, T1> action, T0 ctx0, T1 ctx1 ) {
            Closure.Check( action );
            return new ActionClosure {
                _closure = new Closure {
                    _0 = SValue.Writer<T0>.invoke( ctx0 ),
                    _1 = SValue.Writer<T1>.invoke( ctx1 ),
                    _delegate = action
                },
                _wrapper = ActionClosureWrapper<T0, T1>._default
            };
        }

        public static ActionClosure Create<T0, T1, T2>( Action<T0, T1, T2> action, T0 ctx0, T1 ctx1, T2 ctx2 ) {
            Closure.Check( action );
            return new ActionClosure {
                _closure = new Closure {
                    _0 = SValue.Writer<T0>.invoke( ctx0 ),
                    _1 = SValue.Writer<T1>.invoke( ctx1 ),
                    _2 = SValue.Writer<T2>.invoke( ctx2 ),
                    _delegate = action
                },
                _wrapper = ActionClosureWrapper<T0, T1, T2>._default
            };
        }

        public static ActionClosure Create<T0, T1, T2, T3>( Action<T0, T1, T2, T3> action, T0 ctx0, T1 ctx1, T2 ctx2, T3 ctx3 ) {
            Closure.Check( action );
            return new ActionClosure {
                _closure = new Closure {
                    _0 = SValue.Writer<T0>.invoke( ctx0 ),
                    _1 = SValue.Writer<T1>.invoke( ctx1 ),
                    _2 = SValue.Writer<T2>.invoke( ctx2 ),
                    _3 = SValue.Writer<T3>.invoke( ctx3 ),
                    _delegate = action
                },
                _wrapper = ActionClosureWrapper<T0, T1, T2, T3>._default
            };
        }
    }

    public struct FuncClosure {

        Closure _context;
        Func<Closure, SValue> _wrapper;

        public void Reset() {
            _wrapper = null;
            _context.Reset();
        }

        public T Invoke<T>() {
            if ( _wrapper != null ) {
                var s = _wrapper( _context );
                return SValue.Reader<T>.invoke( ref s );
            }
            return default( T );
        }

        public void Invoke() {
            if ( _wrapper != null ) {
                _wrapper( _context );
            }
        }

        public static FuncClosure Create<TResult>( Func<TResult> func ) {
            Closure.Check( func );
            return new FuncClosure {
                _context = new Closure {
                    _delegate = func
                },
                _wrapper = FuncClosureWrapper<TResult>._default
            };
        }

        public static FuncClosure Create<T, TResult>( Func<T, TResult> func, T ctx ) {
            Closure.Check( func );
            return new FuncClosure {
                _context = new Closure {
                    _0 = SValue.Writer<T>.invoke( ctx ),
                    _delegate = func
                },
                _wrapper = FuncClosureWrapper<T, TResult>._default
            };
        }

        public static FuncClosure Create<T0, T1, TResult>( Func<T0, T1, TResult> func, T0 ctx0, T1 ctx1 ) {
            Closure.Check( func );
            return new FuncClosure {
                _context = new Closure {
                    _0 = SValue.Writer<T0>.invoke( ctx0 ),
                    _1 = SValue.Writer<T1>.invoke( ctx1 ),
                    _delegate = func
                },
                _wrapper = FuncClosureWrapper<T0, T1, TResult>._default
            };
        }

        public static FuncClosure Create<T0, T1, T2, TResult>( Func<T0, T1, T2, TResult> func, T0 ctx0, T1 ctx1, T2 ctx2 ) {
            Closure.Check( func );
            return new FuncClosure {
                _context = new Closure {
                    _0 = SValue.Writer<T0>.invoke( ctx0 ),
                    _1 = SValue.Writer<T1>.invoke( ctx1 ),
                    _2 = SValue.Writer<T2>.invoke( ctx2 ),
                    _delegate = func
                },
                _wrapper = FuncClosureWrapper<T0, T1, T2, TResult>._default
            };
        }

        public static FuncClosure Create<T0, T1, T2, T3, TResult>( Func<T0, T1, T2, T3, TResult> func, T0 ctx0, T1 ctx1, T2 ctx2, T3 ctx3 ) {
            Closure.Check( func );
            return new FuncClosure {
                _context = new Closure {
                    _0 = SValue.Writer<T0>.invoke( ctx0 ),
                    _1 = SValue.Writer<T1>.invoke( ctx1 ),
                    _2 = SValue.Writer<T2>.invoke( ctx2 ),
                    _3 = SValue.Writer<T3>.invoke( ctx3 ),
                    _delegate = func
                },
                _wrapper = FuncClosureWrapper<T0, T1, T2, T3, TResult>._default
            };
        }

        public static FuncClosure Create( Action func ) {
            Closure.Check( func );
            return new FuncClosure {
                _context = new Closure {
                    _delegate = func
                },
                _wrapper = e => { e.Invoke(); return SValue.nil; }
            };
        }

        public static FuncClosure Create<T>( Action<T> func, T ctx ) {
            Closure.Check( func );
            return new FuncClosure {
                _context = new Closure {
                    _0 = SValue.Writer<T>.invoke( ctx ),
                    _delegate = func
                },
                _wrapper = FuncClosureWrapper_VoidResult<T>._default
            };
        }

        public static FuncClosure Create<T0, T1>( Action<T0, T1> func, T0 ctx0, T1 ctx1 ) {
            Closure.Check( func );
            return new FuncClosure {
                _context = new Closure {
                    _0 = SValue.Writer<T0>.invoke( ctx0 ),
                    _1 = SValue.Writer<T1>.invoke( ctx1 ),
                    _delegate = func
                },
                _wrapper = FuncClosureWrapper_VoidResult<T0, T1>._default
            };
        }

        public static FuncClosure Create<T0, T1, T2>( Action<T0, T1, T2> func, T0 ctx0, T1 ctx1, T2 ctx2 ) {
            Closure.Check( func );
            return new FuncClosure {
                _context = new Closure {
                    _0 = SValue.Writer<T0>.invoke( ctx0 ),
                    _1 = SValue.Writer<T1>.invoke( ctx1 ),
                    _2 = SValue.Writer<T2>.invoke( ctx2 ),
                    _delegate = func
                },
                _wrapper = FuncClosureWrapper_VoidResult<T0, T1, T2>._default
            };
        }

        public static FuncClosure Create<T0, T1, T2, T3>( Action<T0, T1, T2, T3> func, T0 ctx0, T1 ctx1, T2 ctx2, T3 ctx3 ) {
            Closure.Check( func );
            return new FuncClosure {
                _context = new Closure {
                    _0 = SValue.Writer<T0>.invoke( ctx0 ),
                    _1 = SValue.Writer<T1>.invoke( ctx1 ),
                    _2 = SValue.Writer<T2>.invoke( ctx2 ),
                    _3 = SValue.Writer<T3>.invoke( ctx3 ),
                    _delegate = func
                },
                _wrapper = FuncClosureWrapper_VoidResult<T0, T1, T2, T3>._default
            };
        }
    }

    internal class ActionClosureWrapper<T> { internal static Action<Closure> _default = e => e.Invoke<T>(); }
    internal class ActionClosureWrapper<T0, T1> { internal static Action<Closure> _default = e => e.Invoke<T0, T1>(); }
    internal class ActionClosureWrapper<T0, T1, T2> { internal static Action<Closure> _default = e => e.Invoke<T0, T1, T2>(); }
    internal class ActionClosureWrapper<T0, T1, T2, T3> { internal static Action<Closure> _default = e => e.Invoke<T0, T1, T2, T3>(); }
    internal class FuncClosureWrapper<TResult> { internal static Func<Closure, SValue> _default = e => e.SRInvoke<TResult>(); }
    internal class FuncClosureWrapper<T, TResult> { internal static Func<Closure, SValue> _default = e => e.SRInvoke<T, TResult>(); }
    internal class FuncClosureWrapper<T0, T1, TResult> { internal static Func<Closure, SValue> _default = e => e.SRInvoke<T0, T1, TResult>(); }
    internal class FuncClosureWrapper<T0, T1, T2, TResult> { internal static Func<Closure, SValue> _default = e => e.SRInvoke<T0, T1, T2, TResult>(); }
    internal class FuncClosureWrapper<T0, T1, T2, T3, TResult> { internal static Func<Closure, SValue> _default = e => e.SRInvoke<T0, T1, T2, T3, TResult>(); }
    internal class FuncClosureWrapper_VoidResult<T> { internal static Func<Closure, SValue> _default = e => { e.Invoke<T>(); return SValue.nil; }; }
    internal class FuncClosureWrapper_VoidResult<T0, T1> { internal static Func<Closure, SValue> _default = e => { e.Invoke<T0, T1>(); return SValue.nil; }; }
    internal class FuncClosureWrapper_VoidResult<T0, T1, T2> { internal static Func<Closure, SValue> _default = e => { e.Invoke<T0, T1, T2>(); return SValue.nil; }; }
    internal class FuncClosureWrapper_VoidResult<T0, T1, T2, T3> { internal static Func<Closure, SValue> _default = e => { e.Invoke<T0, T1, T2, T3>(); return SValue.nil; }; }
}
//EOF
