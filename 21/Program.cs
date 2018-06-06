using System;

namespace _21
{
    class Program
    {


            public static bool Winner(Player player1, Player dealer)
            {
                if(player1.genScore()>= 21 || dealer.genScore() >= 21){
                    return false;
                }else{
                    return true;
                }
            }
        static void Main(string[] args)
        {
            var deck = new Deck();
            var dealer = new Player("AI", deck);
            System.Console.WriteLine("Welcome please input your name: \n");
            string player1Name = Console.ReadLine();
            var player1 = new Player(player1Name, deck);
            var welcomeStatement = String.Format(@"Welcome {0} lets play some Black Jack", player1.name);
            System.Console.WriteLine(welcomeStatement);

            var game = new GamePlay(player1, dealer, deck);
            game.Play();
            var p1 = player1.genScore();
            var ai = dealer.genScore();
            player1.displayHand();
            dealer.displayHand();
            if(p1 > 21){
                System.Console.WriteLine("Dealer wins You lose");
            }else if(p1 <=21 && (ai > 21 || p1 > ai)){
                System.Console.WriteLine("You win congratulations");
            }else{
                System.Console.WriteLine("AI wins the round!");
            }
            System.Console.WriteLine("Thanks for playing");
        }
    }
}
