int computerScore = 0;
int playerScore = 0;

int maxGap = 2;
int scoreToWin = 5;

Console.ForegroundColor = ConsoleColor.Yellow;
Fight();

void Fight()
{
    while (true)
    {
        // Проводим атаку с одно и другой стороны
        Attack(Attacker.Player);
        Attack(Attacker.Computer);

        // Проверяем количество очков, если кто-то набрал раньше scoreToWin, то прекращаем игру,
        // если у оба набрали scoreToWin, то продолжаем игру и ждем, когда кто-то наберем больше другого на maxGap

        Console.WriteLine($"After this round Computer score is {computerScore} & Player score is {playerScore}");
        if (computerScore > scoreToWin && playerScore > scoreToWin)
        {
            // Вычитаем очки одного из очков другого, убираем минус, за счет модуля .Abs и проверяем, что число не >= гапу

            if (Math.Abs(computerScore - playerScore) >= maxGap) 
            {
                // Найден победитель
            ShowWinner();
                break;
            }
            // Играем дальше
        }
        else if ((computerScore == scoreToWin && playerScore != scoreToWin) || (playerScore == scoreToWin && computerScore != scoreToWin))
        {
            // Один набрал, другой не набрал.
            // Найден победитель
            ShowWinner();
            break;
        }
        else continue;
    }
}

void ShowWinner()
{
    string message = "The winner is: ";
    message += computerScore > playerScore ? "Computer" : "Player";
    Console.WriteLine(message);
}

// Возвращаем TRUE, если атака успешная
// Возвращаем FALSE, если атака не удалась
bool Attack(Attacker attacker)
{
    string message = attacker == Attacker.Player ?
        "Your turn to attack. Whitch part you want attack:" + Environment.NewLine :
        "Your turn to defend. Witch part you want to make safe: " + Environment.NewLine;

    message += "1. Head" + Environment.NewLine
        + "2. Chest" + Environment.NewLine
        + "3. Foot" + Environment.NewLine
        + "Your choice: ";
    Console.Write(message);

    var playerAnswer = Console.ReadLine();
    while (true)
    {
        if (int.TryParse(playerAnswer, out _) == false) // знак _ обозначает, что нам не нужно получать спаршенное значение
        {
            Console.Write("Bad input. Try again. Your choice[1-3]: ");
            playerAnswer = Console.ReadLine();
            continue;
        }
        break;
    }

    // Сгенерируем то, что будет атаковано/защищено ОТ attacker'a
    var oppositeResult = new Random().Next(1, 4);
    message = attacker == Attacker.Player ? "Computer choose to defend: " : "Computer choose to attack: ";

    switch (oppositeResult)
    {
        case 1:
            message += "Head";
            break;
        case 2:
            message += "Chest";
            break;
        case 3:
            message += "Foot";
            break;
    }
    message += Environment.NewLine;

    if (Convert.ToInt32(playerAnswer) != oppositeResult)
    {
        message += attacker == Attacker.Player ?
            "Your attack succeed! Your current score is: " + ++playerScore :
            "Your defense failed! Computer current score is: " + ++computerScore;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message + Environment.NewLine);
        Console.ForegroundColor = ConsoleColor.Yellow;
        return true;
    }

    Console.ForegroundColor = ConsoleColor.Red;
    message += attacker == Attacker.Player ?
            "Your attack failed! Your current score is: " + playerScore :
            "Your defense succeed! Computer current score is: " + computerScore;
    Console.WriteLine(message + Environment.NewLine);
    Console.ForegroundColor = ConsoleColor.Yellow;
    return false; // Атака не удалась
}

enum Attacker
{
    Computer,
    Player
}