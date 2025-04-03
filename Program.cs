using System;

interface InterfazJugador
{
    char Simbolo { get; }
    void Jugar(char[,] tablero);
}

class JugadorHumano : InterfazJugador
{
    public char Simbolo { get; private set; }

    public JugadorHumano(char simbolo)
    {
        Simbolo = simbolo;
    }

    public void Jugar(char[,] tablero)
    {
        int fila, columna;
        do
        {
            Console.Write($"Jugador con {Simbolo}, ingrese fila y columna (Ejemplo: 1 2) (Ingrese primero la fila, luego presione ENTER para ingresar la columna): ");
            fila = int.Parse(Console.ReadLine());
            columna = int.Parse(Console.ReadLine());
        } while (tablero[fila, columna] != ' ');
        
        tablero[fila, columna] = Simbolo;
    }///
}

class JugadorIA : InterfazJugador
{
    private Random random = new Random();
    public char Simbolo { get; private set; }

    public JugadorIA(char simbolo)
    {
        Simbolo = simbolo;
    }

    public void Jugar(char[,] tablero)
    {
        int fila, columna;
        do
        {
            fila = random.Next(3);
            columna = random.Next(3);
        } while (tablero[fila, columna] != ' ');
        
        tablero[fila, columna] = Simbolo;
        Console.WriteLine($"Jugador {Simbolo} (IA) jugó en {fila} y {columna}");
    }
}

class Juego
{
    private char[,] tablero;
    private InterfazJugador jugador1;
    private InterfazJugador jugador2;
    private InterfazJugador jugadorActual;
    
    public Juego()
    {
        tablero = new char[3, 3];
        jugador1 = new JugadorHumano('X');
        jugador2 = new JugadorIA('O');
        jugadorActual = jugador1;
        InicializarTablero();
    }
    
    private void InicializarTablero()
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                tablero[i, j] = ' ';
    }
    
    public void Iniciar()
    {
        while (true)
        {
            MostrarTablero();
            jugadorActual.Jugar(tablero);
            
            if (VerificarGanador(jugadorActual.Simbolo))
            {
                MostrarTablero();
                Console.WriteLine($"El jugador {jugadorActual.Simbolo} ha ganado");
                break;
            }
            else if (TableroLleno())
            {
                MostrarTablero();
                Console.WriteLine("Empate..");
                break;
            }
            
            jugadorActual = (jugadorActual == jugador1) ? jugador2 : jugador1;
        }
    }
    
    private void MostrarTablero()
    {
        Console.WriteLine("  0 1 2");
        for (int i = 0; i < 3; i++)
        {
            Console.Write(i + " ");
            for (int j = 0; j < 3; j++)
            {
                Console.Write(tablero[i, j]);
                if (j < 2) Console.Write("|");
            }
            Console.WriteLine();
            if (i < 2) Console.WriteLine("  -----");
        }
    }
    
    private bool VerificarGanador(char simbolo)
    {
        for (int i = 0; i < 3; i++)
            if ((tablero[i, 0] == simbolo && tablero[i, 1] == simbolo && tablero[i, 2] == simbolo) ||
                (tablero[0, i] == simbolo && tablero[1, i] == simbolo && tablero[2, i] == simbolo))
                return true;
        
        if ((tablero[0, 0] == simbolo && tablero[1, 1] == simbolo && tablero[2, 2] == simbolo) ||
            (tablero[0, 2] == simbolo && tablero[1, 1] == simbolo && tablero[2, 0] == simbolo))
            return true;
        
        return false;
    }
    
    private bool TableroLleno()
    {
        foreach (var celda in tablero)
            if (celda == ' ')
                return false;
        return true;
    }
}

class Program
{
    static void Main()
    {
        Juego juego = new Juego();
        juego.Iniciar();
    }
}
