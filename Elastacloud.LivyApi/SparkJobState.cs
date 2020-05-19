namespace Elastacloud.LivyApi
{
   public enum SparkJobState
   {
      /// <summary>
      /// Session has not been started
      /// </summary>
      NotStarted,

      /// <summary>
      /// Session is starting
      /// </summary>
      Starting,

      /// <summary>
      /// Session is starting
      /// </summary>
      Running,

      /// <summary>
      /// Session is waiting for input
      /// </summary>
      Idle,

      /// <summary>
      /// Session is executing a statement
      /// </summary>
      Busy,

      /// <summary>
      /// Session is shutting down
      /// </summary>
      ShuttingDown,

      /// <summary>
      /// Session errored out
      /// </summary>
      Error,

      /// <summary>
      /// Session has exited
      /// </summary>
      Dead,

      /// <summary>
      /// Session is successfully stopped
      /// </summary>
      Success
   }
}