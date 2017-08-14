using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using FGDKit.Base;

public class Test : MonoBehaviour {

	void Update () {
        var action = ActionClosure.Create( ( a, b ) => UDebug.Print( a + b ), 1, 2 );
        var function = FuncClosure.Create( ( a, b ) => a + b, 1, 2 );
        action.Invoke();
        var c = function.Invoke<int>();

        var function2 = FuncClosure.Create( ctx => ctx.Item1 + ctx.Item2, STuple.Create( 1, 2 ) );
        var c2 = function2.Invoke<int>();
	}
}
