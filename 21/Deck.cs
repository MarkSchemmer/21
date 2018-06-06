using System;
using System.Linq;
using System.Collections.Generic;

namespace _21
{
    public class Card
    {
            public string stringVal { get; set; }
            public string suit { get; set; }
            public int val { get; set; }

            public Card(string stringVal, string suit, int val)
            {
                this.stringVal = stringVal;
                this.suit = suit;
                this.val = val;
            }
    }

    public class Deck
    {
            static  List<string> suits = new List<string>() {"Clubs", "Spades", "Hearts", "Diamonds"};
            static Random rnd = new Random();
            static  Dictionary<string, int> dic = new Dictionary<string, int>()
            {
                {"2",2}, {"3",3}, {"4",4},
                {"5",5}, {"6",6}, {"7",7},
                {"8",8}, {"9",9}, {"10",10}, {"Jack",11},
                {"Queen",11}, {"King",11}, {"Ace",11}
            };
             List<Card> memory = new List<Card>();
             List<Card> resetDeck = new List<Card>();
             List<Card> cards { get; set; }

            public Deck()
            {
                var newList = new List<Card>();
                foreach (var item in suits)
                {
                    foreach (var it in dic)
                    {
                        newList.Add(new Card(it.Key, item, it.Value));
                    }
                }

                this.cards = shuffle(newList);
                this.resetDeck.AddRange(newList);
            } 

            public void display()
            {
                this.cards.ForEach(x => 
                    {   
                        var str = String.Format("{0} {1}", x.stringVal, x.suit);
                        Console.WriteLine(str);
                    });

                    System.Console.WriteLine(this.cards.Count());
            }

            public Card deal()
            {
                var card = this.cards[0];
                this.memory.Add(card);
                this.cards.RemoveAt(0);
                return card;
            }

            List<Card> shuffle(List<Card> deck)
            {
                var de = new HashSet<Card>();
                while(de.Count()<52)
                  de.Add(deck[rnd.Next(0,52)]);
                return de.ToList();
            }

            List<Card> reset(List<Card> deck)
            {
                 this.memory = new List<Card>();
                 return shuffle(this.resetDeck);
            }
    }

    public class Player
    {
        public string name { get; set; }
        public List<Card> Hand = new List<Card>();
        private Deck deck {get; set;}

        public Player(string name, Deck deck)
        {
            this.name = name;
            this.deck = deck;
        }

        public void discard(Card card)
        {
            var item = this.Hand.SingleOrDefault(x => x.stringVal == card.stringVal);
            if(item != null)
                Hand.Remove(item);
        }

        public void StartingHand()
        {
            for(var i = 0; i < 2; i++)
            {
                this.Hand.Add(deck.deal());
            }
           
        }

        public void Hit() => this.Hand.Add(deck.deal());
        

        public void DiscardAllCards()
        {
            foreach(var card in this.Hand)
            {
                discard(card);
            }
        }
        public int genScore() => this.Hand.Aggregate(0, (acc, cur) => acc + cur.val);
        

        public void displayHand()
        {
            var display = "Hey "+this.name + " Your current hand \n";
            foreach(var item in this.Hand)
            {
                    display += String.Format("{0} {1}, ", item.stringVal, item.suit);
            }
            display += "     Current score :  " + genScore() + "\n\n\n\n";
            System.Console.WriteLine(display);
        }


    }

    public class GamePlay
    {
        public Player player1 { get; set; }
        public Player AI { get; set; }

        public Deck deck { get; set; }

        public bool AIDone = false;

        public int turns = 0;

        public GamePlay(Player player1, Player AI, Deck deck){
            this.player1 = player1;
            this.AI = AI;
            this.deck = deck;
        }


        public string DisplayHitQuestion(Player player)
        {
            return player.name+" Would you like another Hit? Yes(y)/No(n) or End turn(e)";
        }

        public void Play()
        {
            this.player1.Hit();
            this.AI.Hit();
            this.player1.Hit();
            while(true)
            {
                if(player1.genScore() > 21){
                    break;
                }

                if(this.turns == 0) {player1.displayHand();}
                System.Console.WriteLine(DisplayHitQuestion(player1));
                var hit = Console.ReadLine();
                if(hit == "y"){
                    this.player1.Hit();
                    this.player1.displayHand();
                }

                if(this.AI.genScore() < 18){
                    this.AI.Hit();
                }else{
                    this.AIDone = true;
                }

                if(hit=="e" && this.AIDone){
                    player1.displayHand();
                    AI.displayHand();
                    break;
                }
                this.turns++;
            }
        }
    }

}