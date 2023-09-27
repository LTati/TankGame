interface ITank
{
    public int Armor { get; set; } // броня
    public int Hp { get; set; } // жизни
    public int Damage { get; set; } // урон
    public int Bullet { get; set; } // патроны

    void BuyPatron(); // купить патроны
    void Attack(ITank enemy); // выстрел
    void Cure(); // Лечение
}

interface Icomputer
{
    void Print();
}

class Gamer : ITank
{
    private int starthp;
    public int Armor { set; get; } // броня
    public int Hp { set; get; } // жизни
    public int Damage { set; get; } // урон
    public int Bullet { set; get; } // патроны

    public Gamer(int arm, int h, int dam, int bull)
    {
        Armor = arm;
        Hp = h;
        Damage = dam;
        Bullet = bull;
        starthp = h;
    }

    public void BuyPatron()
    {
        Console.WriteLine($"\nПокупка патронов +5. Текущее количество: {Bullet + 5}");
        Bullet += 5;

    }
    public void Attack(ITank enemy)
    {
        Console.WriteLine("\nВаша атака\n");
        if (Bullet == 0) Console.WriteLine("\n!!!Необходимо купить патроны!!!\n");
        else
        {
            Bullet -= 1;
            var randm = new Random();
            double k = randm.NextDouble();
            if (k <= 0.1)
            {
                int rrr = (int)(Damage * 1.2) - enemy.Armor;
                enemy.Hp -= rrr;
                Console.WriteLine($"\n\t===Вы нанесли Критический выстрел===\t\n Нанесено урона {rrr}");
            }
            else if (0.1 < k && k <= 0.3)
            {
                Console.WriteLine("\n\t ПРОМАХ \t\n");
            }
            else
            {
                int rrr = Damage - enemy.Armor;
                enemy.Hp -= rrr;
                Console.WriteLine($"\n Нанесено урона {rrr}\n");
            }
        }
    }

    public void Cure()
    {
        if (Hp + 10 >= starthp) Hp = starthp;
        else
        {
            Hp += 10;
            Console.WriteLine($"\n Лечение: {Hp}");
        }
        Console.WriteLine($"\n Текущее кол-во жизней {Hp}");
    }
}

class Enemy : ITank, Icomputer
{
    public int Starthp { get; }

    public int Armor { set; get; } // броня
    public int Hp { set; get; } // жизни
    public int Damage { set; get; } // урон
    public int Bullet { set; get; } // патроны

    public Enemy(int arm, int h, int dam, int bull)
    {
        Armor = arm;
        Hp = h;
        Damage = dam;
        Bullet = bull;
        Starthp = h;
    }

    public void Print()
    {
        Console.WriteLine("\n\tХод компьютера\t\n");
    }
    public void Attack(ITank enemy)
    {
        Console.WriteLine("\nАтака соперника\n");
        if (Bullet == 0) BuyPatron();
        else
        {
            Bullet -= 1;
            var randm = new Random();
            double k = randm.NextDouble();
            if (k <= 0.1)
            {
                Console.WriteLine("\n\t===Вам нанесли Критический выстрел===\t\n");
                enemy.Hp -= (int)(Damage * 1.2) - enemy.Armor;
                Console.WriteLine($"- {(int)(Damage * 1.2) - enemy.Armor}\n");
            }
            else if (0.1 < k && k <= 0.3)
            {
                Console.WriteLine("\n\t ПРОМАХ Соперника\t\n");
            }
            else
            {
                enemy.Hp -= Damage - enemy.Armor;
                Console.WriteLine($"\n Нанесено урона {Damage - enemy.Armor}\n");
            }
        }
    }
    public void Cure()
    {
        Console.WriteLine("\nСоперник лечится\n");
        if (Hp + 10 >= Starthp) Hp = Starthp;
        else
        {
            Hp += 10;
            Console.WriteLine($"\n Лечение: {Hp}");
        }
    }
    public void BuyPatron()
    {
        Console.WriteLine("\nПокупка патронов компьютером\n");
        Bullet += 5;
    }
}

class Program
{
    static void Main()
    {
        Gamer gamer = new Gamer(10, 20, 30, 3);
        Enemy enemy = new Enemy(1, 200, 12, 3);

        Random rand = new Random();
        while (gamer.Hp > 0 && enemy.Hp > 0)
        {
            Console.WriteLine($"My tank health: {gamer.Hp}, bullet count: {gamer.Bullet}");
            Console.WriteLine($"Enemy tank health: {enemy.Hp}, bullet count: {enemy.Bullet}");
            Console.WriteLine("Выберети требуемое действие: \n 1.Огонь \n 2.Ремонт \n 3.Купить патроны");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.NumPad1:
                    if (gamer.Bullet == 0) Console.WriteLine("\n !!!Необходимо купить патроны!!!");
                    else gamer.Attack(enemy);
                    break;

                case ConsoleKey.NumPad2:
                    gamer.Cure();
                    break;

                case ConsoleKey.NumPad3:
                    gamer.BuyPatron();
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Неправильное действие");
                    continue;
            }
            enemy.Print();
            int r = rand.Next(1, 3);
            if (r == 2 && (enemy.Hp < enemy.Starthp))
                enemy.Cure();
            else
                if (enemy.Bullet > 0) enemy.Attack(gamer);
            else enemy.BuyPatron();
            Console.WriteLine("\nНажмите любую клавишу чтобы продолжить");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine('\n');
        }

        Console.WriteLine("game over");
        if (gamer.Hp <= 0) Console.WriteLine("Вы проиграли");
        else Console.WriteLine("Вы выиграли");
        Console.WriteLine($"gamer life {gamer.Hp} vs enemy life {enemy.Hp}");
        Console.ReadLine();
    }
}