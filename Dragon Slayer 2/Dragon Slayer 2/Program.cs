using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon_Slayer_2
{
     public enum Team { Good, Bad }
     public enum PlayerType {Knight, Chupacabra, Dragon, Godzilla}
     public enum MainWeapon { Sword, Kick, Fire, Bite }
     public enum SecondWeapon { FireBall, PoisonSpit, DeathJump, Electric }
     public enum Special { FirstAidKit, WTF_It_Was, SpinAround, DrinkSeaWater }
     public enum GameType { History, TwoPlayersHistory, OnePLayerTeam, TwoVsTwo, Skirmish}

     public class Creature
     {
         public int[] HPvalue = new int[] { 120, 115, 200, 240 };
         public enum Controller {Player, AI}
         public string Name { get; set; }
         public int HP { get; set; }
         public bool isAlive { get; set; }
         public int Level { get; set; }
         public PlayerType plType { get; set; }
         public Controller pController { get; set; }
         public List<Weapon> weapons { get; set; }
         public List<int> Upgrades { get; set; } // List of 4: 0) - HP, 1) -Weapon1, 2)-Weapon 2, 3) - Special
         public int numSpecial { get; set; }
         public int playerID { get; set; }
         public Team Team { get; set; }


         public Creature(string name, PlayerType pType, Controller contr)
         {
             int playertype = (int)pType;
             this.plType = pType;
             this.HP = HPvalue[playertype];
             this.pController = contr;
             this.Name = name;
             this.Level = 1;
             this.Upgrades = new List<int>();
             for (int i = 0; i < 4; i++)
             {
                 Upgrades.Add(1);
             }
             this.isAlive = true;
        }
         public void Respawn()
         {
             int playertype = (int)this.plType;
             this.HP = HPvalue[playertype];
             this.isAlive = true;
             this.HP += (this.Upgrades[0]-1) * 2;
             this.numSpecial += 3+ this.Upgrades[3] / 10;
         }
     }

    public class Weapon
    {
      public Enum weaponclass;
      public int damage;
      public int chance;
      public int accuracy;
    }
    public class PreLoadEnemy
    {
        public PlayerType type;
        public string Name;
        public int Level;
        public Team Team; 

        public PreLoadEnemy(PlayerType pType, string name, int Lvl, Team team)
        {
            this.type = pType;
            this.Name = name;
            this.Level = Lvl;
            this.Team = team;
            
        }
    }
   
    public class Graphic
    {
        List<string> StaticBuffer = new List<string>();
        List<string> DynamicBuffer = new List<string>();

  
        public void DrawScene()
        {
            Console.Clear();
            foreach (var item in StaticBuffer)
            {
                Console.WriteLine(item);
            }
            foreach (var item in DynamicBuffer)
            {
                Console.WriteLine(item);
            }
        }

        public void DrawText(string st)
        {
            DynamicBuffer.Add(st);
        }
        public void DrawCanvas(string st)
        {
            StaticBuffer.Add(st);
        }

        public void ResetDynamicBuffer()
        {
            DynamicBuffer.Clear();
        }

        public void ResetCanvasBuffer()
        {
            StaticBuffer.Clear();
        }

        public void DrawPlayers(List<Creature> players)
        {
            this.ResetCanvasBuffer();
            string line = "";
            for (int i = 0; i < 11; i++)
            {
                line = "";
                for (int j = 0; j < players.Count; j++)
                {
                    line += " " + DrawPlayer(players[j], i) + " ";
                }
                this.DrawCanvas(line);
            }
            DrawScene();
        
        }

        public string DrawPlayer(Creature player, int line)
        {
            string value = "";
            List<string> Scene = new List<string>(); // LATER: transfer to this fucntion library of scenes.
            
            Scene.Add(CompleteString (player.Name));
            Scene.Add(CompleteString (player.plType.ToString()));
            Scene.Add(CompleteString ("Level " + player.Level));
            Scene.Add(CompleteString ("HP:" + player.HP + "/" + (player.HPvalue[(int)player.plType] + (player.Upgrades[0]-1)*2)));

            //Draw hp
            string tmp = "";
            int hp_vlaue = Convert.ToInt32(Convert.ToDouble(player.HP) / Convert.ToDouble(player.HPvalue[(int)player.plType] + (player.Upgrades[0]-1)*2) * 12);
            if (hp_vlaue == 12)
                tmp = "************";
            else
            {
                
                for (int i = 1; i < hp_vlaue; i++)
                {
                    tmp += "*";
                }
                for (int i = hp_vlaue; i < 12; i++)
                {
                    tmp += "-";
                }
               
            }
            Scene.Add(tmp);
            Scene.Add("          ");

            if (player.plType == PlayerType.Knight) //
            {
                Scene.Add("     O      ");
                Scene.Add("    /|\\     ");
                Scene.Add("     |      ");
                Scene.Add("    / \\     ");   
            }

            if (player.plType == PlayerType.Dragon)
            {
                Scene.Add("   /\\_/\\    ");
                Scene.Add("  ( q p )   ");
                Scene.Add("    \\_/     ");
                Scene.Add("            ");
            
            }
            if (player.plType == PlayerType.Godzilla)
            {
                Scene.Add("   /\\/\\/\\    ");
                Scene.Add("  ( q  p )   ");
                Scene.Add("   \\____/    ");
                Scene.Add("             ");

            }

            if (player.plType == PlayerType.Chupacabra)
            {
                Scene.Add("    \\|/    ");
                Scene.Add("\\__(. .)__/");
                Scene.Add("   /\\_/\\   ");
                Scene.Add("  /\\   /\\  ");

            }

            Scene.Add("------------");

            value = Scene[line];
            return value;
        }
        private string CompleteString(string line)
        {
            string value = line;
            for (int i = 0; i < (12-line.Length); i++)
            {
                value += " ";
            }
            return value;
        }
    }


    public class DragonSlayer2Game
     {
         private Random rnd = new Random();
         private int[] WeaponDamageList = new int[] { 29, 7, 90,    20, 5, 100,   26, 2, 100,  //Knight
                                                      35,10,80,     25,10,70,     20,2,100,    //Chupa
                                                      14,10,90,     15,5,80,      10,2,100,    //Dragon
                                                      10,15,90,     11,6,80,      13,3,100};   //Godzilla

         //public enum PlayerType {     Knight,      Chupacabra,   Dragon,     Godzilla }
         //public enum MainWeapon {     Sword,       Kick,         Fire,       Bite }
         //public enum SecondWeapon {   FireBall,    PoisonSpit,   DeathJump,  Electric }
         //public enum Special {        FirstAidKit, WTF_It_Was,   SpinAround, DrinkSeaWater }

         private List<Weapon> weapons { get; set; }
         public List<Creature> players { get; set; }
         public Graphic GUI = new Graphic();
         public int round = 1;
         List<List<PreLoadEnemy>> GameSequence = new List<List<PreLoadEnemy>>();
         public GameType GameType { get; set; } 


         public DragonSlayer2Game ()
         {
             players = new List<Creature>();
             weapons = new List<Weapon>();

            // Create weaponList from enums and weaponDamageList
             for (int i = 0; i < 4; i++)
             {
                 Weapon weapon = new Weapon();
                 weapon.weaponclass = (MainWeapon)i;
                 weapon.damage = WeaponDamageList[i*9];
                 weapon.chance = WeaponDamageList[i * 9 + 2];
                 weapon.accuracy = WeaponDamageList[i * 9 + 1];
                 weapons.Add(weapon);

                 weapon = new Weapon();
                 weapon.weaponclass = (SecondWeapon)i;
                 weapon.damage = WeaponDamageList[i * 9+3];
                 weapon.chance = WeaponDamageList[i * 9 + 2+3];
                 weapon.accuracy = WeaponDamageList[i * 9 + 1+3];
                 weapons.Add(weapon);

                 weapon = new Weapon();
                 weapon.weaponclass = (Special)i;
                 weapon.damage = WeaponDamageList[i * 9 + 6];
                 weapon.chance = WeaponDamageList[i * 9 + 2 + 6];
                 weapon.accuracy = WeaponDamageList[i * 9 + 1 + 6];
                 weapons.Add(weapon);
             }
          }

         public int GeneratePlayerID()
         {
             if (players.Count > 0)
                 return players.Select(x => x.playerID).OrderBy(x => x).Last() + 1;
             else return 100;
         }

         public void AddNewPlayer(string name, PlayerType pType, Creature.Controller contr , Team team, int level)
         {
             Creature player = new Creature(name, pType, contr);
             // Give him weapon
             GiveWeapons(player);
             player.Team = team;
             player.playerID = GeneratePlayerID();

             for (int i = 1; i < level; i++)
             {
                 player.Upgrades[rnd.Next(0, 4)]+=10;
             }
             player.Level = level;
             player.Respawn();
             players.Add(player);   
         }

         public void AddRandomPlayer(string name, Creature.Controller contr, int Level, Team team)
         {
             PlayerType pType = (PlayerType)rnd.Next(0, 4);
             AddNewPlayer(name, pType, contr, team, Level);
         }

         public void GiveWeapons(Creature player)
         {
             player.weapons = new List<Weapon>();
             player.weapons.Add(weapons[((int)player.plType)*3]);
             player.weapons.Add(weapons[((int)player.plType)*3 + 1]);
             player.weapons.Add(weapons[((int)player.plType)*3 + 2]);
         }

         public void AddExistingPlayer(Creature player)
         {
             player.playerID = GeneratePlayerID();
             players.Add(player);
         }

         // Choose enemies - logic of weapon action
         public List<int> PlayerChoseEnemy(Creature player, Weapon weapon)
         {
             List<Creature> toAttack = new List<Creature>();

             if (weapon.weaponclass.ToString() == Special.FirstAidKit.ToString()
                 || weapon.weaponclass.ToString() == Special.DrinkSeaWater.ToString())
               {
                   toAttack.Add(players.Where(x => x.playerID == player.playerID).First());
               }
             else // SpinAround of Dragon and WTF of Chupacabra attack ALL enemies
                 {
                     foreach (var item in players.Where(x=>x.Team != player.Team).Where(x=> x.isAlive))
	                {
                        toAttack.Add(item);
	                }
                 }

             if (toAttack.Count > 1)
             {
             // IDEA  - create player inside of game and give player weapon
             if (player.pController == Creature.Controller.Player)
                 {
                     for (int i = 0; i < toAttack.Count; i++)
                     {
                         GUI.DrawText((i + 1) + ") " + toAttack[i].Name + " " + toAttack[i].plType.ToString());
                     }
                         GUI.DrawText(player.Name + ", choose your enemy you want to attack: ");
                         GUI.DrawScene();
                     int enemy = Convert.ToInt32(Console.ReadLine())-1;
                     GUI.ResetDynamicBuffer();
                     if (enemy > toAttack.Count - 1) enemy = 0;
                     toAttack = toAttack.Where(x => x.playerID == toAttack[enemy].playerID).ToList();
                 }
             else
                 {
                     toAttack = players.Where(x => x.Team != player.Team).ToList();
                 }
             }

             List<int> IDs = toAttack.Select(x=> x.playerID).ToList();
             return IDs;
         }

         public int ChooseWeapon(Creature player)
         {
            
             int value = 0;
             if (player.pController == Creature.Controller.Player)
             {
                 //
                 for (int i = 0; i < 2; i++)
                 {
                     GUI.DrawText((i + 1) + ") Attack with " + player.weapons[i].weaponclass.ToString());
                 }
                 if (player.numSpecial > 0)
                     GUI.DrawText("3) Use " + player.weapons[2].weaponclass.ToString() + ", " + player.numSpecial + " left");

                 GUI.DrawText(player.Name + ", choose your weapon: ");
                 GUI.DrawScene();
                 value = ChooseOption(3);
                 GUI.ResetDynamicBuffer();
             }
             else
             {
                 // Use special or not
                 if (player.numSpecial > 0 && player.HP < player.HPvalue[(int)player.plType]/2)
                     value = rnd.Next(1, 4);
                 else
                     value = rnd.Next(1, 3);
             }
            // return player.weapons[value - 1];
             return value - 1;
         }

         public void Attack(Creature player)
         {
            int weaponID = ChooseWeapon(player);
            Weapon weapon = player.weapons[weaponID];
             
             List<int> Ids = PlayerChoseEnemy(player, weapon);

             if (Ids.Count != 0 && player.HP > 0)
             {
             // Calculate chance;
                 int chance = rnd.Next(1, 101);
                 if (chance < weapon.chance)
             {
                 //Calculate Damage for common attacks
                 int damage = weapon.damage + rnd.Next(1, weapon.accuracy) + rnd.Next(player.Upgrades[weaponID + 1], 2 * player.Upgrades[weaponID + 1]);

                 if (weapon.weaponclass.ToString() == Special.FirstAidKit.ToString()
                    || weapon.weaponclass.ToString() == Special.DrinkSeaWater.ToString())
                 {
                     player.HP += damage;
                     GUI.DrawText(player.Name + " " + player.plType.ToString() + " healed himself with "
                         + weapon.weaponclass.ToString() + " by " + damage + " HP");
                 }

                  // Chupacabra attack
                 else if (weapon.weaponclass.ToString() == Special.WTF_It_Was.ToString())
                 {
                     player.HP += damage;
                     GUI.DrawText(player.Name + " " + player.plType.ToString() + " healed himself with "
                      + weapon.weaponclass.ToString() + " by " + damage + " HP");
                     damage = damage / Ids.Count;
                     foreach (var item in Ids)
                     {
                         Creature enemy = players.Where(x => x.playerID == item).First();
                         enemy.HP -= damage;
                         GUI.DrawText(player.Name + " " + player.plType.ToString() + " hit " +
                           enemy.Name + " " + enemy.plType.ToString() + " with "
                        + weapon.weaponclass.ToString() + " by " + damage + " HP");
                     }
                 }
                 // Chapacarba Posion Hit 
                 else if (weapon.weaponclass.ToString() == SecondWeapon.PoisonSpit.ToString())
                 {
                     Creature enemy = players.Where(x => x.playerID == Ids[0]).First();
                     enemy.HP -= (enemy.HP / 4 + rnd.Next(1, 2*player.Upgrades[weaponID+1]));
                     GUI.DrawText(player.Name + " " + player.plType.ToString() + " hit " +
                           enemy.Name + " " + enemy.plType.ToString() + " with "
                        + "Poison Spit and injured by 1/3 of health by" + (enemy.HP / 3) + " HP");
                 }
                 // Godzilla Electric
                 else if (weapon.weaponclass.ToString() == SecondWeapon.Electric.ToString())
                 {
                     Creature enemy = players.Where(x => x.playerID == Ids[0]).First();
                     enemy.HP -= damage;
                     GUI.DrawText(player.Name + " " + player.plType.ToString() + " hit " +
                           enemy.Name + " " + enemy.plType.ToString() + " with "
                        + "Electric punch by " + damage + " HP");

                     int count = 2;
                     foreach (var item in players.Where(x => x.Team != player.Team).Where(x => x.playerID != enemy.playerID))
                     {
                         item.HP -= damage / count;
                         GUI.DrawText(player.Name + " " + player.plType.ToString() + " hit " +
                               item.Name + " " + item.plType.ToString() + " with "
                            + "Electric punch by " + (damage / count) + " HP");
                         count++;
                     }
                 }
                 // Common attacks
                 else
                 {
                     int id = rnd.Next(0, Ids.Count);
                     Creature enemy = players.Where(x => x.playerID == Ids[id]).First();
                     enemy.HP -= damage;
                     GUI.DrawText(player.Name + " " + player.plType.ToString() + " hit " +
                           enemy.Name + " " + enemy.plType.ToString() + " with "
                       + weapon.weaponclass.ToString() + " by " + damage + " HP");
                 }

             }
             else GUI.DrawText(player.Name + " " + player.plType.ToString() + " used " + weapon.weaponclass.ToString() + " and missed");

                 if (weaponID == 2) player.numSpecial--;
             }
         }

         public int ChooseOption(int numOpt)
         {
             int value = 0;
             string input = "some";
             bool isnum = false;
             while (!isnum)
             {
                 input = Console.ReadLine();
                 isnum = true;
                 if (input.Length == 0) isnum = false;
                 foreach (var item in input)
                 {
                     if (!char.IsNumber(item))
                         isnum = false;
                 }
                 if (isnum)
                 {
                     value = Convert.ToInt32(input);
                     if (value > numOpt) isnum = false;
                 }
             }
             return value;

         }

        //One round
         public void Round()
         {
             //GUI.ResetDynamicBuffer();
             foreach (var item in players)
             {
                 if (item.isAlive)
                   this.Attack(item);
             }
             GUI.DrawScene();
         }

         public void RespawnPlayers()
         {
             foreach (var item in players)
             {
                 item.Respawn();
             }
         }

      // One battle - several rounds
         public Team StartBattle(int battleNumber)
         { 
             
             bool End = false;
             Team winTeam = Team.Bad;
             GUI.DrawPlayers(players);
             
             while (!End)
             {
                 GUI.DrawPlayers(players);
                 Round();

                 // Who died raise your hand!
                 foreach (var item in players)
                 {
                     if (item.HP <= 0)
                     {
                         item.HP = 0;
                         item.isAlive = false;
                     }
                 }

                 GUI.DrawPlayers(players);

                 // Check if team won or not
                 if (!players.Where(x => x.Team == Team.Good).Where(x => x.isAlive).Any())
                 {
                     End = true;
                     winTeam = Team.Bad;
                 }
                 else if (!players.Where(x => x.Team == Team.Bad).Where(x => x.isAlive).Any())
                 {
                     End = true;
                     winTeam = Team.Good;
                 }
             }

             return winTeam;
         }
         public void GenerateGameConsequence(GameType gametype) 
         {
             if (gametype == GameType.History)
             {
                 for (int i = 1; i <= 10; i++)
                 {
                     List<PreLoadEnemy> battle = new List<PreLoadEnemy>();
                     if (i <= 4)
                     {
                         battle.Add(new PreLoadEnemy((PlayerType)rnd.Next(0, 4), "Weak", i, Team.Bad));
                     }

                     if (i == 5)
                     {
                         battle.Add(new PreLoadEnemy((PlayerType)rnd.Next(0, 4), "Enemy", 2, Team.Bad));
                         battle.Add(new PreLoadEnemy((PlayerType)rnd.Next(0, 4), "Weak", 1, Team.Bad));
                     }

                     if (i == 6)
                     {
                         battle.Add(new PreLoadEnemy((PlayerType)rnd.Next(0, 4), "Enemy", 3, Team.Bad));
                         battle.Add(new PreLoadEnemy((PlayerType)rnd.Next(0, 4), "Weak", 1, Team.Bad));
                     }
                     if (i == 7)
                     {
                         battle.Add(new PreLoadEnemy((PlayerType)rnd.Next(0, 4), "Enemy", 3, Team.Bad));
                         battle.Add(new PreLoadEnemy((PlayerType)rnd.Next(0, 4), "Weak", 2, Team.Bad));
                     }


                     if (i == 8)
                     {
                         battle.Add(new PreLoadEnemy((PlayerType)rnd.Next(0, 4), "Serious", 9, Team.Bad));
                     }

                     if (i == 9)
                     {
                         battle.Add(new PreLoadEnemy((PlayerType)rnd.Next(0, 4), "Heavy", 5, Team.Bad));
                         battle.Add(new PreLoadEnemy((PlayerType)rnd.Next(0, 4), "Weak", 2, Team.Bad));
                     }

                     if (i == 10)
                     {
                         battle.Add(new PreLoadEnemy((PlayerType)rnd.Next(0, 4), "BadAss", 12, Team.Bad));
                     }
                     GameSequence.Add(battle);
                 }
             }

             else if (gametype == GameType.OnePLayerTeam)
             {
                
                 for (int i = 1; i <= 40; i++)
                 {
                     List<PreLoadEnemy> battle = new List<PreLoadEnemy>();   
                      battle.Add(new PreLoadEnemy((PlayerType)rnd.Next(0, 4), "TeamMate", i, Team.Good));
                      battle.Add(new PreLoadEnemy((PlayerType)rnd.Next(0, 4), "Enemy#1", i, Team.Bad));
                      battle.Add(new PreLoadEnemy((PlayerType)rnd.Next(0, 4), "Enemy#2", i, Team.Bad));
                      this.GameSequence.Add(battle);
                 }
             }
         
         }

        // DEBUG and Balance SECTION

         public void Debug(PlayerType pl1, PlayerType pl2, int pl1lvl, int pl2lvl)
         {
             for (int k = 0; k < 4; k++)
             {
                 PlayerType pl = (PlayerType)k;
                 
            
             int pl1Win = 0;
             int pl2Win = 0;

             for (int j = 1; j <= 10; j++)
             {
                 pl1Win = 0;
                 pl2Win = 0;
                 for (int i = 0; i < 1000; i++)
                 {
                     this.players.Clear();
                     this.AddNewPlayer("pl1", pl1, Creature.Controller.AI, Team.Good, j);               

                       this.AddNewPlayer("pl2", pl, Creature.Controller.AI, Team.Bad, j);
                     Team win = this.StartBattleDebug();
                     if (win == Team.Bad) 
                         pl2Win++;
                     else pl1Win++;
                 }
                 Console.WriteLine(pl1 + " " + j + " VS " + pl + "  " + j);
                 Console.WriteLine(pl1Win + " " + pl2Win);
             }
             }
             Console.ReadLine();
           
         }
         public Team StartBattleDebug()
         {

             bool End = false;
             Team winTeam = Team.Bad;
             int whoAttack = rnd.Next(1, 3);

             while (!End)
             {


                 RoundDebug(whoAttack);
                 // Who died raise your hand!
                 foreach (var item in players)
                 {
                     if (item.HP <= 0)
                     {
                         item.HP = 0;
                         item.isAlive = false;
                     }
                 }

                 // Check if team won or not
                 if (!players.Where(x => x.Team == Team.Good).Where(x => x.isAlive).Any())
                 {
                     End = true;
                     winTeam = Team.Bad;
                 }
                 else if (!players.Where(x => x.Team == Team.Bad).Where(x => x.isAlive).Any())
                 {
                     End = true;
                     winTeam = Team.Good;
                 }
             }

             return winTeam;
         }
         public void RoundDebug(int whoattack)
         {
             if
                    (whoattack == 1)
             {
                 foreach (var item in players)
                 {
                     if (item.isAlive)
                         this.Attack(item);
                 }
             }
             else
                 for (int i = players.Count - 1; i >= 0; i--)
                 {
                     if (players[i].isAlive)
                         this.Attack(players[i]);
                 }
         }

         public void AddPlayersToGame(int number)
         {
             for (int j = 1; j <= number; j++)
             {    
             // Add player to game
             Console.WriteLine("Player " + j+", Enter your name: ");
             string pName = Console.ReadLine();
             if (pName == "") pName = "Player " + j.ToString();
             Console.Clear();
             // Load player from save?
             for (int i = 0; i < 4; i++)
             {
                 Console.WriteLine((i + 1) + ") " + (PlayerType)i);
             }
             Console.WriteLine(pName + ", choose your hero: ");
             int input = ChooseOption(4) - 1;
             this.AddNewPlayer(pName, (PlayerType)input, Creature.Controller.Player, Team.Good, 1);
             Console.WriteLine();
             }
         }
         public void ChooseGameType()
         {
             Console.WriteLine("1) 1 PLayer History Mode");
             Console.WriteLine("2) 2 Players History Mode - NOT IMPLEMENTED");
             Console.WriteLine("3) 1 PLayer Team Battles");
             Console.WriteLine("4) 2 PLayers Vs 2 Battles - NOT IMPLEMENTED");
             Console.WriteLine("5) Skirmish - NOT IMPLEMENTED");
             Console.WriteLine("Choose the game type:");
             int input = ChooseOption(5)-1;
             // list only only implemented game types
             if (input != 0 & input != 2) input = 0; 
             this.GameType = (GameType)input;
         }

         public void LevelUP(Creature player)
         {
             Console.WriteLine(player.Name + ", you've got a LEVEL UP!");
             Console.WriteLine("1) Increase HP");
             Console.WriteLine("2) Increase " + player.weapons[0].weaponclass.ToString() + " power");
             Console.WriteLine("3) Increase " + player.weapons[1].weaponclass.ToString() + " power");
             Console.WriteLine("4) Increase " + player.weapons[2].weaponclass.ToString() + " power");
             int input = this.ChooseOption(4)-1;
             player.Upgrades[input] += 10;
             player.Level++;
         }
        //MAIN FUNCTION OF GAME
         public void CreateGame()
         {
             // BEGIN OF GAME
             int numOfBattle = 1;

             Console.WriteLine("Welcome to Dargon Slayer 2");

             ChooseGameType();
            
             //Add players
             if (GameType == GameType.TwoPlayersHistory || GameType == GameType.TwoVsTwo)
                 AddPlayersToGame(2);
             else AddPlayersToGame(1);

             //CREATE HISTORY 
             GenerateGameConsequence(this.GameType);
             
             // BATLLE!
             bool wishToContinue = true;
             while (wishToContinue)
             {
                 players = players.Where(x => x.pController == Creature.Controller.Player).ToList();
                 foreach (var item in players)
                 {
                     item.Respawn();
                 }

                     foreach (var item in GameSequence[numOfBattle - 1])
                     {
                         this.AddRandomPlayer(item.Name, Creature.Controller.AI, item.Level, item.Team);
                     }
             

      




                 Team teamWin = StartBattle(numOfBattle);
                 Console.WriteLine(teamWin + " team win, do you want to continue? Y/N" );
                 string w = Console.ReadLine();
                 if (w.ToLower() == "y" || w.ToLower() == "yes")
                 {
                     wishToContinue = true;
                     if (teamWin == Team.Good)
                     {
                         numOfBattle++;
                         foreach (var item in players.Where(x => x.pController == Creature.Controller.Player))
                         {
                             LevelUP(item);
                         }
                     }
                 }

             }


             Console.WriteLine("Goodbuy, " + players[0].Name);
             Console.ReadLine();
            //..next round till team lose
        //
         // ask if wanna play -> Start level (next);
             // create

         }
     }

    class Program
    {

        static void Main(string[] args)
        {

            Creature ct = new Creature("John", PlayerType.Knight, Creature.Controller.Player);
            Creature ct1 = new Creature("Nick", PlayerType.Knight, Creature.Controller.Player);
            DragonSlayer2Game game = new DragonSlayer2Game();
            game.CreateGame();
          //  game.Debug(PlayerType.Knight, PlayerType.Dragon, 10, 10);

            game.GUI.DrawScene();
           // ct.DoAttack(game.players);
            //ct.ChooseAttack();
            Console.Read();
        }
    }
}