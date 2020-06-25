using System;
using System.Collections.Generic;

namespace GameServer
{
    /// <summary>
    /// Proporciona una implementación común para todos los
    /// elementos del juego.
    /// </summary>
    public abstract class GameItem : IGameItem
    {
        /// <summary>
        /// Crea un elemento del juego en (0,0) y con valor 0.
        /// </summary>
        public GameItem() : this(null, 0) { }

        /// <summary>
        /// Crea un elemento del juego.
        /// </summary>
        /// <param name="coords">fila y columna en la que posiciona el elemento. Si es null, el elemento se posiciona en las coordenadas (0,0)</param>
        /// <param name="myValue">valor asignado al elemento</param>
        public GameItem(ItemCoordinates coords, int myValue)
        {
            if (coords != null)
            {
                Coords = coords;
            }
            else
            {
                Coords = new ItemCoordinates();
            }
            MyValue = myValue;
        }

        public override string ToString()
        {
            return this.GetType().ToString() + this.Coords;
        }

        public ItemCoordinates Coords { get; set; }
        public int MyValue { get; set; }

        public abstract IGameItem GetDeepCopy();
    }


    class Fruit : GameItem
    {
        public Fruit() : base()
        {
            MyValue = 2;
        }
        public Fruit(ItemCoordinates coords, int myValue) : base(coords, myValue) { }

        public override IGameItem GetDeepCopy()
        {
            Fruit v = new Fruit(new ItemCoordinates(Coords), MyValue);
            return v;
        }
    }

    class Poisson : GameItem
    {
        public Poisson() : base()
        {
            MyValue = -1;
        }

        public Poisson(ItemCoordinates coords, int myValue) : base(coords, myValue) { }

        public override IGameItem GetDeepCopy()
        {
            Poisson v = new Poisson(new ItemCoordinates(Coords), MyValue);
            return v;
        }
    }

    /// <summary>
    /// Implementa al personaje Mouse, cuyo objetivo es comer fruta (instancias de Fruit) y 
    /// evitar venenos (Poisson) y gatos (Cat)
    /// </summary>
    class Mouse : GameItem, IMovableItem
    {
        public Mouse() : base()
        {
            CurrentDirection = Direction.Rigth;
        }
        public Mouse(ItemCoordinates coords, int myValue) : base(coords, myValue) { }

        public override IGameItem GetDeepCopy()
        {
            Mouse v = new Mouse(new ItemCoordinates(Coords), MyValue);
            v.CurrentDirection = CurrentDirection;
            return v;
        }

        public Direction next = Direction.Rigth;
        public Direction CurrentDirection
        {
            get
            {
                return next;
            }
            set
            {
                next = value;
            }
        }

        /// <summary>
        /// No cambia su dirección. La dirección se mantiene constante salvo
        /// que otro objeto la cambie un cliente a través de CurrentDirection.
        /// </summary>
        public virtual void ChangeDirection()
        {

        }
    }

    class SmartMouse : Mouse
    {
        List<IGameItem> items = new List<IGameItem>();

        public SmartMouse() : base()
        {
            CurrentDirection = Direction.Rigth;
        }
        public SmartMouse(ItemCoordinates coords, int myValue) : base(coords, myValue) { }

        public override IGameItem GetDeepCopy()
        {
            SmartMouse v = new SmartMouse(new ItemCoordinates(Coords), MyValue);
            v.CurrentDirection = CurrentDirection;
            return v;
        }

        public override void ChangeDirection()
        {
            if (items == null)
            {
                return;
            }
            foreach (var item in items)
            {
                if (item is Fruit)
                {
                    if (item.Coords.Columna != Coords.Columna)
                    {
                        CurrentDirection = (item.Coords.Columna > Coords.Columna) ? Direction.Rigth : Direction.Left;
                    }
                    else if (item.Coords.Fila != Coords.Fila)
                    {
                        CurrentDirection = (item.Coords.Fila > Coords.Fila) ? Direction.Down : Direction.Up;
                    }
                }

                if (item is Fruit)
                {
                    if (item.Coords.Columna != Coords.Columna)
                    {
                        CurrentDirection = (item.Coords.Columna > Coords.Columna) ? Direction.Rigth : Direction.Left;
                    }
                    else if (item.Coords.Fila != Coords.Fila)
                    {
                        CurrentDirection = (item.Coords.Fila > Coords.Fila) ? Direction.Down : Direction.Up;
                    }
                }
            }
        }

        private List<Cat> getCats()
        {
            List<Cat> cats = new List<Cat>();
            foreach (var i in items)
            {
                if (i is Cat)
                {
                    cats.Add((Cat)i);
                }
            }
            return cats;
        }

        /*
        private Cat closestCat()
        {
            List<Cat> lista = getCats();
            Cat[] cats = lista.ToArray();
            Cat cat = null;
            double dist = 0;

            if (cats != null)
            {
                if (cats.Length != 0)
                {
                    cat = cats[0];
                    for (int i = 1; i < cats.Length; i++)
                    {
                        int f = (cats[i].Coords.Fila - Coords.Fila);
                        int c = (cats[i].Coords.Fila - Coords.Fila);
                        double d = Math.Sqrt(f * f + c * c);
                        if (d < dist)
                        {
                            dist = d;
                            cat = cats[i];
                        }
                    }
                }
            }
            return cat;
        }
        */

        public virtual void UpdateGame(List<IGameItem> elements)
        {
            if (elements == null)
            {
                return;
            }
            foreach (var item in elements)
            {
                this.items.Add(item);
            }
        }
    }

    /// <summary>
    /// Implementa al personaje gato, cuyo objetivo es comer ratones (Mouse).
    /// </summary>
    class Cat : GameItem, IMovableItem
    {
        List<IGameItem> items = new List<IGameItem>();

        int turno = 1;

        public Cat() : base()
        {
            CurrentDirection = Direction.Rigth;
        }
        public Cat(ItemCoordinates coords, int myValue) : base(coords, myValue) { }

        public override IGameItem GetDeepCopy()
        {
            Cat v = new Cat(new ItemCoordinates(Coords), MyValue);
            v.CurrentDirection = CurrentDirection;
            return v;
        }

        private Direction next = Direction.Down;
        public Direction CurrentDirection
        {
            get
            {
                return next;
            }
            set
            {
                next = value;
            }
        }

        /// <summary>
        /// Cada vez que se invoca cambia aleatoriamente la dirección de movimiento del gato
        /// hacia la izquierda, la derecha o hacia abajo. Nunca hacia arriba.
        /// </summary>
        public virtual void ChangeDirection()
        {
            if (items == null)
            {
                return;
            }

            foreach (var item in items)
            {
                if (item is Mouse)
                {
                    if (item.Coords.Columna != Coords.Columna && turno % 2 == 0)
                    {
                        CurrentDirection = (item.Coords.Columna > Coords.Columna) ? Direction.Rigth : Direction.Left;
                    }
                    else if (item.Coords.Fila != Coords.Fila)
                    {
                        CurrentDirection = (item.Coords.Fila > Coords.Fila) ? Direction.Down : Direction.Up;
                    }
                }
                turno++;
            }


        }

        public virtual void UpdateGame(List<IGameItem> elements)
        {
            if (elements == null)
            {
                return;
            }
            foreach (var item in elements)
            {
                this.items.Add(item);
            }
        }
    }
}