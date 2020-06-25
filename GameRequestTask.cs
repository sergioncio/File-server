using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameServer
{
    /// <summary>
    /// Modela los métodos que pueden ser llamados una vez que se completa la descarga de un juego
    /// </summary>
    /// <param name="request"></param>
    public delegate void RequestCompletionHandlers(GameRequestTask request);

    /// <summary>
    /// Modela una tarea que puede ser encolada en un servidor y asignada a un thread para
    /// su ejecución.
    /// </summary>
    public class GameRequestTask
    {
        // servidor en el que se delega la ejecución del servicio en el método execute.
        private IGamesProvider server;

        private static int contadorDePeticionesDeServicios = 1;
        private string requestId;
        public string RequestId { get { return requestId; } }
        private string gameId;

        private bool isFinished = false;
        public bool IsFinished { get { return isFinished; } }

        // Aquí se van guardando los frames que se leen del servidor
        private List<List<IGameItem>> result = new List<List<IGameItem>>();

        // Lista de manejadores de fin de servicio.
        public RequestCompletionHandlers completionHandlers;

        /// <summary>
        /// Construye una nueva tarea
        /// </summary>
        /// <param name="server">Servidor utilizado pra realizar el servicio</param>
        /// <param name="gameId">Identificador del juego solicitado</param>
        /// <param name="resultHandler">Handler que será llamado al finalizar el servicio</param>
        public GameRequestTask(
            IGamesProvider server, string gameId, RequestCompletionHandlers resultHandler)
        {

            this.requestId = gameId + "_" + contadorDePeticionesDeServicios.ToString();

            // Complete el código del constructor.
            // No olvide añadir resultHandler a completionHandlers.

            // Añada código aquí.
            this.isFinished = IsFinished;
            this.server = server;
            this.gameId = gameId;
            if (resultHandler != null)
            {
                completionHandlers += resultHandler;
            }
        }

        /// <summary>
        /// Devuelve el juego descargado. El valor devuelto sólo será válido una vez terminado
        /// el servicio.
        /// </summary>
        /// <returns></returns>
        public List<List<IGameItem>> getResult()
        {
            return result;
        }

        /// <summary>
        /// Devuelve true si el juego existe en el servidor.
        /// </summary>
        public bool CanBeExecuted
        {
            get
            {
                return server.ExistGame(gameId);
            }
        }

        /// <summary>
        /// Encapsula el código que descarga el juego del servidor y lo almacena en result.
        /// Al terminar la descarga del juego se invocan los manejadores de fin de servicio si existe alguno.
        /// </summary>

        public void ExecuteRequest()
        {
            int counter = 0;

            if (server.ExistGame(gameId))
            {
                Console.WriteLine("GameRequestTask: game exists");
                List<IGameItem> f = null;

                // Recoja los frames uno a uno con GetFrame 
                // y vaya añadiéndolos a result

                // Añada el código aquí.

                while ((f = server.GetFrame(gameId, counter)) != null)
                {
                    result.Add(f);
                    counter++;


                }

                // Marque el servicio como acabado y llame a los manejadores 
                // de fin de servicio que se hayan suscrito.

                // Añada el código aquí.
                isFinished = true;
                if (completionHandlers != null)
                {
                    completionHandlers(this);
                }
                Console.WriteLine("GameRequestTask: END SERVICE !!!!!!!!!!!! " + counter + " FRAMES");

            }
        }
    }
}