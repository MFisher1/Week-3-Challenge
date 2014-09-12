using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon_Slayer_2
{
     public enum PlayerType {Knight, Chupacabra, Dragon, Godzilla}

     public class Creature
     {
         private int[] HPvalue = new int[] { 100, 140, 20, 250 };
         //private int[] firstWeaponDamage = new int[] {25,5,70,};

         private enum MainWeapon {Sword, Kick, Fire, Bite}
         private enum SecondWeapon {FireBall, Poison, DeathJump, Electric}
         private enum Special { FirstAidKit, WTF_It_Was, SpinAround, DrinkSeaWater }
         public enum Controller {Player, AI}

         public string Name { get; set; }
         public int HP { get; set; }
         public bool isAlive { get; set; }
         public int Level { get; set; }
         public PlayerType plType { get; set; }
         public Controller pController { get; set; }
         List<Enum> weapons { get; set; }

         public Creature(string name, PlayerType pType, Controller contr)
         {
             int playertype = (int)plType;
             this.Level = 1;
             this.plType = pType;
             this.HP = HPvalue[playertype];
             this.pController = contr;
             weapons = new List<Enum>();
             weapons.Add((MainWeapon)playertype);
             weapons.Add((SecondWeapon)playertype);
             weapons.Add((Special)playertype);
           
        }

         public Enum ChooseAttack()
         {
             int value = 0;
             if (pController == Controller.Player)
             { 
                 for (int i = 0; i < weapons.Count; i++)
			{
                Console.WriteLine((i+1) + ") for use " + weapons[i]);
			}
                 Console.WriteLine("Choose weapon: ");  
                 value = Convert.ToInt32(Console.ReadLine())-1;
             }
             else
             {
                 Random rnd = new Random();
                 value = rnd.Next(0, 3);
             }
             return weapons[value];
         }

         public int DoAttack(List<Creature> players)
         {
             players[0].HP -= 50;
             int value = 0;
             Enum weapon = ChooseAttack();
             return value;
         }
         

     }
     public class DragonSlayer2Game
     {
         public List<Creature> players { get; set; }

         public DragonSlayer2Game ()
         {
             players = new List<Creature>();
          }

     }

    class Program
    {

        static void Main(string[] args)
        {
            //DragonCombatSimalator(1,1,1,1,1);
            Creature ct = new Creature("John", PlayerType.Knight, Creature.Controller.Player);
            DragonSlayer2Game game = new DragonSlayer2Game();
            game.players.Add(ct);
            ct.DoAttack(game.players);
            ct.ChooseAttack();
            Console.Read();
        }
    }
}