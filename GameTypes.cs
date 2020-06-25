using System;
using System.Collections.Generic;

namespace GameServer
{
    /// <summary>
    /// Modela las coordenadas de un elemento del juego en una matriz (fila, columna)
    /// </summary>
    public class ItemCoordinates
    {
        public int Fila { get; set; }
        public int Columna { get; set; }

        public ItemCoordinates() : this(0, 0) { }

        public ItemCoordinates(int fila, int columna)
        {
            Fila = fila; Columna = columna;
        }

        public ItemCoordinates(ItemCoordinates coords)
        {
            if (coords != null)
            {
                Fila = coords.Fila; Columna = coords.Columna;
            }
            else
            {
                Fila = 0; Columna = 0;
            }
        }

        public override string ToString()
        {
            return "[" + Fila + ", " + Columna + "]";
        }
    }

    /// <summary>
    /// Proporciona una interfaz común para todos los elementos
    /// del juego: su posición en una matriz (fila, columna) y un
    /// valor cuya interprestación dependerá de la lógica del
    /// juego.
    /// </summary>
    public interface IGameItem
    {
        ItemCoordinates Coords { get; set; }
        int MyValue { get; set; }
        IGameItem GetDeepCopy();  
    }

    /// <summary>
    /// Define las direcciones en las que puede moverse un elemento del juego.
    /// </summary>
    public enum Direction { Up, Down, Left, Rigth }

    /// <summary>
    /// Define una interfaz para aquellos elementos del juego que pueden moverse
    /// por el tablero (cambiar su fila y su columna).
    /// </summary>
    public interface IMovableItem : IGameItem
    {
        /// <summary>
        /// Establece la dirección en la que se mueve el elemento del juego.
        /// Esta propiedad se ofrece para que otro objeto establezca la dirección de movimiento
        /// del elemento del juego.
        /// </summary>
        Direction CurrentDirection { get; set; }

        /// <summary>
        /// Establece la dirección en la que se mueve el elemento del juego.
        /// Esta propiedad se ofrece para que el propio elemnto del juego
        /// establezca su dirección de movimiento.
        void ChangeDirection();
    }

    /// <summary>
    /// Proporciona una interfaz común para todas las vistas del juego
    /// </summary>
    public interface IGameItemView
    {
        void Draw(System.Windows.Forms.PaintEventArgs e);
    }

    /// <summary>
    /// Proporciona una interfaz común para todos los juegos que pueden ejecutarse desde la ventana ServerUI
    /// </summary>
    public interface IGameLogic
    {
        /// <summary>
        /// Comienza un nuevo juego
        /// </summary>
        /// <returns></returns>
        int StartGame();

        /// <summary>
        /// Termina el juego.
        /// </summary>
        /// <returns></returns>
        int GameOver();

        /// <summary>
        /// Devuelve true si el juego ha terminado.
        /// </summary>
        /// <returns></returns>
        bool IsOver();


        /// <summary>
        /// Ejecuta un paso del juego. En cada paso del juego se actualizan todos los elementos
        /// del juego de acuerdo con la implementación de este método.
        /// </summary>
        /// <returns> >= 0 si el juego puede continuar, menor que cero si el juego
        /// no puede continuar.</returns>
        int ExecuteStep();


        /// <summary>
        /// Devuelve un enumerador que permite iterar sobre todos los elementos del juego.
        /// </summary>
        /// <returns> enumerador que permite iterar sobre todos los elementos del juego</returns>
        IEnumerator<IGameItem> GetEnumerator();
    }

    /// <summary>
    /// Proporciona una interfaz común para todos los escuchadores del juego.
    /// </summary>
    /// <param name="items"></param>
    public delegate void GameObserver(List<IGameItem> items);

}













