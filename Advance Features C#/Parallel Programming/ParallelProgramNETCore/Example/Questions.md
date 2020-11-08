/*
 * 
 * Async and Await programming model 
 * O problema com as .NET calls e que elas normalmente bloqueiam : 
 *   int n = DoWork1(); DoWork2( n );
 * Neste caso, DoWork1() bloqueia completamente a Thread ate que a invocao termine.  
 * Assim, so posso chamar DoWork2() depois de DoWork1() terminar...
 * 
 * QUESTIONS : 
 *  Why should DoWork1() block the current thread ?  (e.g. UI Thread)
 *  R: Yes, we can spawn a thread for the DoWork1() but remember the DoWork2() needs the result of DoWork1().
 *  Why not run DoWork1() in a separate thread and let DoWork2() be a continuation ? (Isto porque DoWork2() precisa do input de DoWork1() )
 *  R: Yes, we can do that EXPLICTILY like so : 
 *      ThreadPool.QueueUserWorkItem(...) or with Task.Factory.StartNew(...) and them we can put 
 *      bar() in a ContinueWith() call 
 *      
 * Why the Async/Wait paradigm was created ? 
 * R : Because we want to use continuations and spawing threads in a more transparent way, recorring to the .NET Framework.
 * 
 * How does async work ? 
 * R: For example, we have the sync method :
 *  int Calculate();
 * and them we change it to be async :
 *  Task<int> CalculateAsync() ;
 * - Add the work Async to indicate to the client that we are making a Task and that this Task can run on a separate Thread in TPL Task Pool
 * - Return Task of the type
 * These methods can have exactly the same body.
 * And now we can consume the method was follows :
 * async void Foo()
 * {
 *   int n = await CalculateAsync();
 * }
 * 
 * What does async do ? 
 *  It enables the use of the await keyword.
 * 
 * What does await do ? 
 *  1) Does not perform a physical wait (blocking )
 *          int foo = await CalculateAsync();
 *      NOT EQUAL 
 *          int foo = CalculateAsync().Result;
 *   This would block the caller - precisely what we want to avoid!
 *  2)  Await coerces the retunr values from a Task (faz o unbox automatico)
 *  3) "The code that follows, is bellow, the AWAIT keyword is a continuation, like continueWith()"
 *  4) Gives up the Current. We not longer do anything on the current thread. 
 *  5) "It pass's the result from Thread 2 to Thread 1" in safe way.
 *  
 *  Se eu tiver dois awaits quando e que o release da Main Thread ? 
 *  No ultimo Await. Conseguimos ver isso no User case 2 e 3 
 */