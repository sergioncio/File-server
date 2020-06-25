using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{

    public class GameLogic:IGameLogic
    {

        public readonly int MaxFila;
        public readonly int MaxColumna;

        // Lista de elementos del juego. 
        private List<IGameItem> gameItems = new List<IGameItem>();

        // Personajes mouse 
        private SmartMouse myMouse; // = new SmartMouse(new ItemCoordinates(20, 1), 0);
        
        // Información de estado
        private int stepCounter = 0;
        private int catCounter = 0;
        private bool gameOver = false;
        private bool turnoGatos = false;

        // eventos del juego
        public GameObserver gameListeners;

        /// <summary>
        /// Construye un tablero de 20 x 40
        /// </summary>
        public GameLogic() : this(20, 40) { }

        /// <summary>
        /// Construye un tablero de filas x columnas
        /// </summary>
        /// <param name="filas"></param>
        /// <param name="columnas"></param>
        public GameLogic(int nF, int nC)
        {
            MaxFila = nF;
            MaxColumna = nC;
        }

        public int StartGame()
        {
            System.Console.WriteLine("GameLogic: StarGame ---> Entering");

            // Reset Data.
            gameListeners = null;
            gameItems.Clear();
            gameOver = false;
            stepCounter = catCounter = 0;

            // Create new Mouse and add it to game.            
            myMouse = new SmartMouse(new ItemCoordinates(MaxFila/2, 1), 0);
            this.AddItem(myMouse);
            gameListeners += myMouse.UpdateGame;

            // Fill board
            FillBoard(5, 5);
            
            return 0;
        }

        public int GameOver()
        {
            gameOver = true;
            return 0;
        }

        public bool IsOver()
        {
            return gameOver;
        }

        /// <summary>
        /// Devuelve true si en la (fila, columna) especificada no hay
        /// ningún elemento de juego.
        /// </summary>
        /// <param name="fila"></param>
        /// <param name="columna"></param>
        /// <returns></returns>
        public Boolean IsCellAvailableAndEmpty(int fila, int columna)
        {
            if (fila >= MaxFila || columna >= MaxColumna || fila < 0)
            {
                return false;
            }
            foreach(var i in gameItems)
            {
                if (i.Coords.Fila == fila && i.Coords.Columna == columna)
                {
                    return false;
                }
            }
            return true;
        }
        

        /// <summary>
        /// Añade un elemento al juego en la celda especificada en las coordenadas del
        /// argumento (item), siempre que (1) item != null, (2) la posición no esté ya
        /// ocupada por otro elemntoe y (3) el elemento no esté ya en el juego.
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(IGameItem item)
        {
            // Comprobar que item es distinto de null
            if (item != null)
            {
                // Comprobar que no está ya e el tablero.
                if (gameItems.Contains(item))
                {
                    return;
                }
                // Comprobar que sus coordenadas están en los límites del tablero
                // columna qué la celda no está ya ocupada.
                if (IsCellAvailableAndEmpty(item.Coords.Fila, item.Coords.Columna))
                {
                    gameItems.Add(item);
                }
            }
        }

        /// <summary>
        /// Elimina un elemento del juego.
        /// </summary>
        /// <param name="item"></param>
        public void RemoveItem(IGameItem item)
        {
            // Comprobar que es distinto de null
            if (item == null)
            {
                return;
            }
            // Borrar de la lista.
            gameItems.Remove(item);
        }

        /// <summary>
        /// Rellena el juego con un número determinado de frutas y venenos colocados en 
        /// posiciones aleatorias.
        /// </summary>
        public void FillBoard(int nFruits, int nPoissons)
        {
            int fruits = nFruits >= 0 ? nFruits : 0;
            int poissons = nPoissons >= 0 ? nPoissons : 0;

            Random rdn = new Random();

            // Se crean columna añaden 10 frutas columna 10 poissons y 2 entradas/salidas.
            for (int i = 0; i < fruits; i++)
            {
                AddItem(
                    new Fruit(
                        new ItemCoordinates(rdn.Next(1, MaxFila - 1), rdn.Next(1, MaxColumna - 1)), 2));
            }
            for (int i = 0; i < poissons; i++)
            {
                AddItem(
                    new Poisson(
                        new ItemCoordinates(rdn.Next(1, MaxFila - 1), rdn.Next(1, MaxColumna - 1)), -1));
            }
            foreach (var i in gameItems)
            {
                Console.WriteLine(i);
            }
        }

        /// <summary>
        /// Devuelve un enumerador de los elementos del juego.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<IGameItem> GetEnumerator()
        {
            foreach (var item in gameItems)
            {
                yield return item;
            }
        }

        /// <summary>
        /// Implementa la lógica del juego que se ejecuta en cada Tick de un temporizador.
        /// </summary>
        /// <returns>>= 0 si el juego puede continuar, un valor negativo si no se puede continuar</returns>
        public int ExecuteStep()
        {
            if (gameOver)
            {
                System.Console.WriteLine("GameLogic: GameOver ---> Se acabo la vida");
                return -1;
            }

            // Mouse processing.
            myMouse.ChangeDirection();
            processNextCell(myMouse);
            if (myMouse.MyValue < 0)
            {
                System.Console.WriteLine("GameLogic: GameOver ---> Mouse encuentra Cat");
                gameOver = true;
                return -1;
            }

            // Cats Processing           
            List<Cat> cats = this.getCats();
            foreach (Cat cat in cats)
            {
                cat.ChangeDirection();
                this.processNextCell(cat);

                if (myMouse.MyValue < 0)
                {
                    System.Console.WriteLine("GameLogic: GameOver ---> Cat encuentra Mouse");
                    gameOver = true;
                    return -1;
                }
            }
            turnoGatos = !turnoGatos;
            

            // AÑADIR ELEMENTOS
            if (catCounter < 5) stepCounter++;
            
            if (!gameOver && stepCounter%50 == 0 && catCounter < 2)
            {
                Cat c = new Cat(new ItemCoordinates(1, 10), 0);
                gameListeners += c.UpdateGame;
                AddItem(c);
                catCounter++;                
            }

            // Actualiza información de observadores del juego
            this.gameListeners(gameItems);

            return 0;
        }

        /// <summary>
        /// Devuelve la siguiente posición del elemento dependiendo
        /// de la dirección de su movimiento.
        /// NO cambia la posicion actual del elemento.
        /// Cuando el elemento llega a un límite del tablero aparece por el lado contrario.
        /// </summary>
        /// <param name="item"></param>
        private ItemCoordinates getNextCoord(IMovableItem item)
        {
            ItemCoordinates nextCoord = new ItemCoordinates(item.Coords);

            if (item.CurrentDirection == Direction.Up)
            {
                nextCoord.Fila = (item.Coords.Fila - 1);
                if (item.Coords.Fila < 0) nextCoord.Fila = MaxFila - 1;
            }
            else if (item.CurrentDirection == Direction.Down)
            {
                nextCoord.Fila = (item.Coords.Fila + 1) % MaxFila;
            }
            else if (item.CurrentDirection == Direction.Rigth)
            {
                nextCoord.Columna = (item.Coords.Columna + 1) % MaxColumna;
            }
            else if (item.CurrentDirection == Direction.Left)
            {
                nextCoord.Columna = (item.Coords.Columna - 1);
                if (item.Coords.Columna < 0) nextCoord.Columna = MaxColumna - 1;
            }
            return nextCoord;
        }

        /// <summary>
        /// Actualiza el juego en función de lo que hay en la celda donde está el ratón.
        /// Si no hay nada, no hace nada.
        /// Si hay fruta o veneno, suma al "valor" del ratón el valor de la fruta (positivo) o del veneno (negativo) y
        /// elimina la fruta o el veneno.
        /// Si hay un gato pone el valor del ratón en -1.
        /// </summary>
        /// <returns> El valor almacenado en el ratón. </returns>
        private void processNextCell(IMovableItem movable)
        {
            ItemCoordinates nextCoord = this.getNextCoord(movable);
            IGameItem item = this.findItemAt(nextCoord);

            if (item != null)
            {
                if ((movable is Mouse && item is Cat) || (movable is Cat && item is Mouse))
                {
                    myMouse.MyValue = -1;
                }
                else if (movable is Mouse && (item is Fruit || item is Poisson))
                {
                    myMouse.MyValue += item.MyValue;
                    RemoveItem(item);
                }
            }
            movable.Coords = new ItemCoordinates(nextCoord);
        }

        /// <summary>
        /// Itera la lista de elementos del juego hasta encontrar uno que esté en las
        /// coordenadas especificadas como argumento
        /// </summary>
        /// <param name="target"></param>
        /// <returns> Elemnto en target o null si no hay nada en esas coordenadas</returns>
        private IGameItem findItemAt(ItemCoordinates target)
        {
            foreach(GameItem i in gameItems)
            {
                if (i.Coords.Fila == target.Fila && i.Coords.Columna == target.Columna)
                {
                    return i;
                }
            }
            return null;
        }

        /// <summary>
        /// Itera la lista de elementos de juego y devuelve una lista con los gatos.
        /// </summary>
        /// <returns></returns>
        private List<Cat> getCats()
        {
            List<Cat> cats = new List<Cat>();
            foreach(var i in gameItems)
            {
                if (i is Cat)
                {
                    cats.Add((Cat)i);
                }
            }
            return cats;
        }
    }
}
