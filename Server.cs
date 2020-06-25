using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GameServer
{
    /// <summary>
    /// Implementa la interfaz IGamesProvider
    /// </summary>
    public class Server : IGamesProvider
    {
        // Los juegos se lamacenan en un diccionario, en el que la clave es el identificador del juego.
        Dictionary<string, List<List<IGameItem>>> recordedGames = new Dictionary<string, List<List<IGameItem>>>();
        Scheduler scheduler = new Scheduler();

        public Server()
        {
            scheduler.Start();
        }

        public bool ExistGame(string gameId)
        {
            return recordedGames.ContainsKey(gameId);
        }

        public List<IGameItem> GetFrame(string gameId, int frameNumber)
        {
            List<IGameItem> frame = null;
            if (ExistGame(gameId))
            {
                Thread.Sleep(100);
                List <List<IGameItem>> b = null;
                if (recordedGames.TryGetValue(gameId, out b))
                {
                    if (b.Count > frameNumber && frameNumber >= 0)
                    {
                        frame = b[frameNumber];
                    }
                }
            }
            return frame;
        }

        // Utiliza GetFrame para la implementación.
        public List<List<IGameItem>> GetGame(string gameId)
        {
            List<List<IGameItem>> b = null;

            if (ExistGame(gameId))
            {
                b = new List<List<IGameItem>>();
                int counter = 0;
                List<IGameItem> frame = null;
                while ((frame = GetFrame(gameId, counter)) != null){
                    b.Add(frame);
                    counter++;
                }
            }
            return b;
        }

        public List<string> GetGameList()
        {
            List<String> ids = new List<string>();

            foreach (var pair in recordedGames)
            {
                ids.Add(pair.Key);
            }
            return ids;
        }

        public void LoadGame(List<List<IGameItem>> game, string gameId)
        {
            if (!recordedGames.ContainsKey(gameId))
            {
                recordedGames.Add(gameId, game);
            }
        }

        public Task<List<List<IGameItem>>> GetGameAsync(string gameId)
        {
            // Retornar la tarea correspondiente, asociada al método GetGame.
            
            return Task.Run<List<List<IGameItem>>>(() =>
            {
                return GetGame(gameId);
            });
        }

        public string IssueGameRequest(String gameId, RequestCompletionHandlers resultHandler)
        {
            Console.WriteLine("Scheduler: IssueGameRequest: " + gameId);

            // Cree la tarea del tipo GameRequestTask, 
            // insértela en la cola del scheduler y retorne su identificador.
            GameRequestTask request = new GameRequestTask(this, gameId, resultHandler);
            scheduler.InsertRequest(request);
            return request.RequestId;
        }
    }

    class Scheduler
    {
        ConcurrentQueue<GameRequestTask> requestsQueue = new ConcurrentQueue<GameRequestTask>();
        bool stop;

        public void InsertRequest(GameRequestTask request)
        {
            Console.WriteLine("Scheduler: Insert new Query");
            requestsQueue.Enqueue(request);
            //Encole la petición.
        }

        public void Start()
        {
            Console.WriteLine("Scheduler. Start ditpaching ...");
            Thread hilo = new Thread(new ThreadStart(run));
            hilo.Start();
            // Arranque un hilo con el método run del scheduler
        }

        public void Stop()
        {
            stop = true;
        }

        public void run()
        {
            while (!stop)
            {
                // desencolar request.
                // si se puede ejecutar, ejecutar la request dsencolada en 
                // un nuevo thread.

                // Añada el código aquí.
                if (requestsQueue.TryDequeue(out GameRequestTask request))
                {
                    if (request.CanBeExecuted)
                    {
                        Thread hilo = new Thread(new ThreadStart(request.ExecuteRequest));
                        hilo.Start();

                    }
                }
            }
        }
    }
}