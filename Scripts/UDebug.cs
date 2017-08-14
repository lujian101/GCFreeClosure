using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FGDKit.Base {

    public static class UDebug {

        delegate void LogDelegate( String msg );
        delegate void LogExceptionDelegate( Exception e );
        static LogDelegate _Log = NoAction;
        static LogDelegate _LogError = NoAction;
        static LogDelegate _LogWarning = NoAction;
        static LogExceptionDelegate _LogException = NoActionException;
        static int _LogCount = 0;
        static int _ErrorCount = 0;
        static int _WarningCount = 0;
        static int _ExceptionCount = 0;
        static int _AssertFailCount = 0;
        static bool _enabled = true;

        public static bool showCallStackEnabled = true;
        public static bool enabled {
            get {
                return _enabled;
            }
            set {
                if ( _enabled != value ) {
                    _enabled = value;
                    if ( _enabled ) {
                        _Log = UnityEngine.Debug.Log;
                        _LogError = UnityEngine.Debug.LogError;
                        _LogException = UnityEngine.Debug.LogException;
                        _LogWarning = UnityEngine.Debug.LogWarning;
                    } else {
                        _Log = NoAction;
                        _LogError = NoAction;
                        _LogWarning = NoAction;
                        _LogException = NoActionException;
                    }
                }
            }
        }

        public static int errorCount {
            get {
                return _ErrorCount;
            }
        }

        public static int warningCount {
            get {
                return _WarningCount;
            }
        }

        public static int assertFailCount {
            get {
                return _AssertFailCount;
            }
        }

        public static int exceptionCount {
            get {
                return _ExceptionCount;
            }
        }

        public static bool hasWarning {
            get {
                return ( _ExceptionCount + _ErrorCount + _AssertFailCount + _WarningCount ) > 0;
            }
        }

        public static bool hasError {
            get {
                return ( _ExceptionCount + _ErrorCount + _AssertFailCount ) > 0;
            }
        }

        public static void ClearLogCounter() {
            Interlocked.Exchange( ref _LogCount, 0 );
            Interlocked.Exchange( ref _ErrorCount, 0 );
            Interlocked.Exchange( ref _WarningCount, 0 );
            Interlocked.Exchange( ref _ExceptionCount, 0 );
            Interlocked.Exchange( ref _AssertFailCount, 0 );
        }

        public delegate bool AssertTest();

        public static void MessageBox( String s ) {
#if UNITY_EDITOR && UNITY_EDITOR_WIN
            if ( showCallStackEnabled ) {
                using ( var prcShell = new System.Diagnostics.Process() ) {
                    prcShell.StartInfo.FileName = UnityEngine.Application.dataPath + "/../../Tool/Bin/MsgBoxer.exe";
                    UDebug.Log( prcShell.StartInfo.FileName );
                    prcShell.StartInfo.Arguments = "\"" + s + "\"";
                    prcShell.Start();
                }
            }
#endif
        }

        public static void ShowCallStack() {
            MessageBox( GetCallStack( 2 ) );
        }

        public static String GetCallStack( int skipFrame = 1 ) {
            StackTrace stackTrace = new StackTrace( skipFrame, true ); // get call stack
            StackFrame[] stackFrames = stackTrace.GetFrames(); // get method calls (frames)
            // write call stack method names
            var sb = new StringBuilder( 200 );
            for ( int i = 0; i < stackFrames.Length; ++i ) {
                var frame = stackFrames[i];
                sb.Append( frame.GetMethod() );
                sb.Append( "(at " );
                sb.Append( frame.GetFileName() );
                sb.Append( ": " );
                sb.Append( frame.GetFileLineNumber() );
                sb.Append( ")\n" );
            }
            return sb.ToString();
        }

        public static int GetCallStackSize() {
            StackTrace stackTrace = new StackTrace( 1, false ); // get call stack
            return stackTrace.FrameCount;
        }

        public static String SafeFormat<T>( String format, T arg ) {
            if ( format != null ) {
                try {
                    return String.Format( format, arg );
                } catch ( Exception e ) {
                    UDebug.LogException( e );
                }
            }
            return String.Empty;
        }

        public static String SafeFormat<T1, T2>( String format, T1 arg1, T2 arg2 ) {
            if ( format != null ) {
                try {
                    return String.Format( format, arg1, arg2 );
                } catch ( Exception e ) {
                    UDebug.LogException( e );
                }
            }
            return String.Empty;
        }

        public static String SafeFormat<T1, T2, T3>( String format, T1 arg1, T2 arg2, T3 arg3 ) {
            if ( format != null ) {
                try {
                    return String.Format( format, arg1, arg2, arg3 );
                } catch ( Exception e ) {
                    UDebug.LogException( e );
                }
            }
            return String.Empty;
        }

        public static String SafeFormat<T1, T2, T3, T4>( String format, T1 arg1, T2 arg2, T3 arg3, T4 arg4 ) {
            if ( format != null ) {
                try {
                    return String.Format( format, arg1, arg2, arg3, arg4 );
                } catch ( Exception e ) {
                    UDebug.LogException( e );
                }
            }
            return String.Empty;
        }

        public static String SafeFormat<T1, T2, T3, T4, T5>( String format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5 ) {
            if ( format != null ) {
                try {
                    return String.Format( format, arg1, arg2, arg3, arg4, arg5 );
                } catch ( Exception e ) {
                    UDebug.LogException( e );
                }
            }
            return String.Empty;
        }

        public static String SafeFormat( String format, params object[] args ) {
            if ( format != null && args != null ) {
                try {
                    return String.Format( format, args );
                } catch ( Exception e ) {
                    UDebug.LogException( e );
                }
            }
            return String.Empty;
        }

        [Conditional( "DEBUG" ), Conditional( "UNITY_EDITOR" ), Conditional( "ENABLE_ASSERTION" )]
        static public void Assert( bool condition, string assertString = null ) {
            if ( !condition ) {
                Interlocked.Increment( ref _AssertFailCount );
#if UNITY_EDITOR
                UnityEngine.Debug.Assert( condition, assertString );
#else
                System.Diagnostics.Debug.Assert( condition, assertString );
#endif
            }
        }

        #region base logger
        [Conditional( "DEBUG" ), Conditional( "UNITY_EDITOR" ), Conditional( "ENABLE_ASSERTION" )]
        public static void Log( String arg ) {
            Interlocked.Increment( ref _LogCount );
            _Log( arg );
        }

        [Conditional( "DEBUG" ), Conditional( "UNITY_EDITOR" ), Conditional( "ENABLE_ASSERTION" )]
        public static void Log<T>( T arg ) {
            Interlocked.Increment( ref _LogCount );
            _Log( arg.ToString() );
        }

        [Conditional( "DEBUG" ), Conditional( "UNITY_EDITOR" ), Conditional( "ENABLE_ASSERTION" )]
        public static void LogError( String arg ) {
            Interlocked.Increment( ref _ErrorCount );
            _LogError( arg );
        }

        [Conditional( "DEBUG" ), Conditional( "UNITY_EDITOR" ), Conditional( "ENABLE_ASSERTION" )]
        public static void LogError<T>( T arg ) {
            Interlocked.Increment( ref _ErrorCount );
            _LogError( arg.ToString() );
        }

        [Conditional( "DEBUG" ), Conditional( "UNITY_EDITOR" ), Conditional( "ENABLE_ASSERTION" )]
        public static void LogWarning( String arg ) {
            Interlocked.Increment( ref _WarningCount );
            _LogWarning( arg );
        }

        [Conditional( "DEBUG" ), Conditional( "UNITY_EDITOR" ), Conditional( "ENABLE_ASSERTION" )]
        public static void LogWarning<T>( T arg ) {
            Interlocked.Increment( ref _WarningCount );
            _LogWarning( arg.ToString() );
        }

        [Conditional( "DEBUG" ), Conditional( "UNITY_EDITOR" ), Conditional( "ENABLE_ASSERTION" )]
        public static void LogException( Exception e ) {
            Interlocked.Increment( ref _ExceptionCount );
            _LogException( e );
        }
        #endregion

        #region LogEx
        [Conditional( "DEBUG" ), Conditional( "UNITY_EDITOR" ), Conditional( "ENABLE_ASSERTION" )]
        public static void Log<T>( String format, T arg ) {
            Log( SafeFormat( format, arg ) );
        }

        [Conditional( "DEBUG" ), Conditional( "UNITY_EDITOR" ), Conditional( "ENABLE_ASSERTION" )]
        public static void Log<T1, T2>( String format, T1 arg1, T2 arg2 ) {
            Log( SafeFormat( format, arg1, arg2 ) );
        }

        [Conditional( "DEBUG" ), Conditional( "UNITY_EDITOR" ), Conditional( "ENABLE_ASSERTION" )]
        public static void Log<T1, T2, T3>( String format, T1 arg1, T2 arg2, T3 arg3 ) {
            Log( SafeFormat( format, arg1, arg2, arg3 ) );
        }

        [Conditional( "DEBUG" ), Conditional( "UNITY_EDITOR" ), Conditional( "ENABLE_ASSERTION" )]
        public static void Log<T1, T2, T3, T4>( String format, T1 arg1, T2 arg2, T3 arg3, T4 arg4 ) {
            Log( SafeFormat( format, arg1, arg2, arg3, arg4 ) );
        }

        [Conditional( "DEBUG" ), Conditional( "UNITY_EDITOR" ), Conditional( "ENABLE_ASSERTION" )]
        public static void Log<T1, T2, T3, T4, T5>( String format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5 ) {
            Log( SafeFormat( format, arg1, arg2, arg3, arg4, arg5 ) );
        }

        [Conditional( "DEBUG" ), Conditional( "UNITY_EDITOR" ), Conditional( "ENABLE_ASSERTION" )]
        public static void Log( String format, params object[] args ) {
            Log( SafeFormat( format, args ) );
        }
        #endregion

        #region LogErrorEx
        [Conditional( "DEBUG" ), Conditional( "UNITY_EDITOR" ), Conditional( "ENABLE_ASSERTION" )]
        public static void LogError<T>( String format, T arg ) {
            LogError( SafeFormat( format, arg ) );
        }

        [Conditional( "DEBUG" ), Conditional( "UNITY_EDITOR" ), Conditional( "ENABLE_ASSERTION" )]
        public static void LogError<T1, T2>( String format, T1 arg1, T2 arg2 ) {
            LogError( SafeFormat( format, arg1, arg2 ) );
        }

        [Conditional( "DEBUG" ), Conditional( "UNITY_EDITOR" ), Conditional( "ENABLE_ASSERTION" )]
        public static void LogError<T1, T2, T3>( String format, T1 arg1, T2 arg2, T3 arg3 ) {
            LogError( SafeFormat( format, arg1, arg2, arg3 ) );
        }

        [Conditional( "DEBUG" ), Conditional( "UNITY_EDITOR" ), Conditional( "ENABLE_ASSERTION" )]
        public static void LogError<T1, T2, T3, T4>( String format, T1 arg1, T2 arg2, T3 arg3, T4 arg4 ) {
            LogError( SafeFormat( format, arg1, arg2, arg3, arg4 ) );
        }

        [Conditional( "DEBUG" ), Conditional( "UNITY_EDITOR" ), Conditional( "ENABLE_ASSERTION" )]
        public static void LogError<T1, T2, T3, T4, T5>( String format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5 ) {
            LogError( SafeFormat( format, arg1, arg2, arg3, arg4, arg5 ) );
        }

        [Conditional( "DEBUG" ), Conditional( "UNITY_EDITOR" ), Conditional( "ENABLE_ASSERTION" )]
        public static void LogError( String format, params object[] args ) {
            LogError( SafeFormat( format, args ) );
        }
        #endregion

        #region LogWarningEx
        [Conditional( "DEBUG" ), Conditional( "UNITY_EDITOR" ), Conditional( "ENABLE_ASSERTION" )]
        public static void LogWarning<T>( String format, T args ) {
            LogWarning( SafeFormat( format, args ) );
        }

        [Conditional( "DEBUG" ), Conditional( "UNITY_EDITOR" ), Conditional( "ENABLE_ASSERTION" )]
        public static void LogWarning<T1, T2>( String format, T1 arg1, T2 arg2 ) {
            LogWarning( SafeFormat( format, arg1, arg2 ) );
        }

        [Conditional( "DEBUG" ), Conditional( "UNITY_EDITOR" ), Conditional( "ENABLE_ASSERTION" )]
        public static void LogWarning<T1, T2, T3>( String format, T1 arg1, T2 arg2, T3 arg3 ) {
            LogWarning( SafeFormat( format, arg1, arg2, arg3 ) );
        }

        [Conditional( "DEBUG" ), Conditional( "UNITY_EDITOR" ), Conditional( "ENABLE_ASSERTION" )]
        public static void LogWarning<T1, T2, T3, T4>( String format, T1 arg1, T2 arg2, T3 arg3, T4 arg4 ) {
            LogWarning( SafeFormat( format, arg1, arg2, arg3, arg4 ) );
        }

        [Conditional( "DEBUG" ), Conditional( "UNITY_EDITOR" ), Conditional( "ENABLE_ASSERTION" )]
        public static void LogWarning<T1, T2, T3, T4, T5>( String format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5 ) {
            LogWarning( SafeFormat( format, arg1, arg2, arg3, arg4, arg5 ) );
        }


        [Conditional( "DEBUG" ), Conditional( "UNITY_EDITOR" ), Conditional( "ENABLE_ASSERTION" )]
        public static void LogWarning( String format, params object[] args ) {
            LogWarning( SafeFormat( format, args ) );
        }
        #endregion

        [Conditional( "DEBUG" ), Conditional( "UNITY_EDITOR" ), Conditional( "ENABLE_ASSERTION" )]
        public static void BreakPoint() {
#if UNITY_EDITOR
            UnityEngine.Debug.DebugBreak();
#else
            System.Diagnostics.Debugger.Break();
#endif
        }

        private static void NoAction( String msg ) {
#if !UNITY_EDITOR
            Console.WriteLine( msg );
#endif
        }

        private static void NoActionException( Exception e ) {
#if !UNITY_EDITOR
            Console.WriteLine( e.ToString() );
#endif
        }

        public static void Print( String arg ) {
#if UNITY_4 || UNITY_5 || UNITY_EDITOR || DEBUG
            UnityEngine.Debug.Log( arg );
#else
            Console.WriteLine( arg );
#endif
        }

        public static void Print<T>( T arg ) {
#if UNITY_4 || UNITY_5 || UNITY_EDITOR || DEBUG
            UnityEngine.Debug.Log( arg.ToString() );
#else
            Console.WriteLine( arg.ToString() );
#endif
        }

        public static void Print<T>( String format, T arg ) {
#if UNITY_4 || UNITY_5 || UNITY_EDITOR || DEBUG
            UnityEngine.Debug.Log( SafeFormat( format, arg ) );
#else
            Console.WriteLine( SafeFormat( format, arg ) );
#endif
        }

        public static void Print<T1, T2>( String format, T1 arg1, T2 arg2 ) {
#if UNITY_4 || UNITY_5 || UNITY_EDITOR || DEBUG
            UnityEngine.Debug.Log( SafeFormat( format, arg1, arg2 ) );
#else
            Console.WriteLine( SafeFormat( format, arg1, arg2 ) );
#endif
        }

        public static void Print<T1, T2, T3>( String format, T1 arg1, T2 arg2, T3 arg3 ) {
#if UNITY_4 || UNITY_5 || UNITY_EDITOR || DEBUG
            UnityEngine.Debug.Log( SafeFormat( format, arg1, arg2, arg3 ) );
#else
            Console.WriteLine( SafeFormat( format, arg1, arg2, arg3 ) );
#endif
        }

        public static void Print<T1, T2, T3, T4>( String format, T1 arg1, T2 arg2, T3 arg3, T4 arg4 ) {
#if UNITY_4 || UNITY_5 || UNITY_EDITOR || DEBUG
            UnityEngine.Debug.Log( SafeFormat( format, arg1, arg2, arg3, arg4 ) );
#else
            Console.WriteLine( SafeFormat( format, arg1, arg2, arg3, arg4 ) );
#endif
        }

        public static void Print<T1, T2, T3, T4, T5>( String format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5 ) {
#if UNITY_4 || UNITY_5 || UNITY_EDITOR || DEBUG
            UnityEngine.Debug.Log( SafeFormat( format, arg1, arg2, arg3, arg4, arg5 ) );
#else
            Console.WriteLine( SafeFormat( format, arg1, arg2, arg3, arg4, arg5 ) );
#endif
        }

        public static void Print( String format, params object[] args ) {
#if UNITY_4 || UNITY_5 || UNITY_EDITOR || DEBUG
            UnityEngine.Debug.Log( SafeFormat( format, args ) );
#else
            Console.WriteLine( format, args );
#endif
        }

        static UDebug() {
            if ( UnityEngine.Debug.isDebugBuild ||
                UnityEngine.Application.isEditor ||
                UnityEngine.Application.isPlaying == false ) {
                enabled = true;
            } else {
                enabled = false;
            }
        }
    }
}
