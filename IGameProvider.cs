using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameServer
{
    /// <summary>
    /// Proporciona la interfaz de los servicios de distribución de juegos.
    /// </summary>
    public interface IGamesProvider
    {
        /// <summary>
        /// Indica si el juego está almacenado en el servidor
        /// </summary>
        /// <param name="gameId">identificador del juego</param>
        /// <returns></returns>
        bool ExistGame(string gameId);

        /// <summary>
        /// Devuelve la lista de los identificadores de los juegos almacenados en el servidor.
        /// </summary>
        /// <returns></returns>
        List<string> GetGameList();

        /// <summary>
        /// Obtiene un frame determinado de un juego determinado.
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="frameNumber"></param>
        /// <returns></returns>
        List<IGameItem> GetFrame(string gameId, int frameNumber);

        /// <summary>
        /// Devuelve todos los frames de un juego.
        /// Cada frame es una lista de GameItems y el juego es una lista de frames.
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        List<List<IGameItem>> GetGame(string gameId);

        /// <summary>
        /// Guarda un juego en el servidor. A partir de ese momento estará disponible
        /// para su descarga.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="gameId"></param>
        void LoadGame(List<List<IGameItem>> game, string gameId);

        /// <summary>
        /// Encola una petición de un juego en el servidor.
        /// </summary>
        /// <param name="gameId">Identificador del juego</param>
        /// <param name="handler">si es distinto de null se invocará al terminar deescargar el juego</param>
        /// <returns>Identificador asignado a la petición de servicio</returns>
        string IssueGameRequest(string gameId, RequestCompletionHandlers handler);

        /// <summary>
        /// Devuelve todos los frames de un juego.
        /// Cada frame es una lista de GameItems y el juego es una lista de frames.
        /// Es similar a GetGame, pero pensado para una IMPLEMENTACIÓN ASÍNCRONA. 
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        Task<List<List<IGameItem>>> GetGameAsync(string gameId);
    }
}