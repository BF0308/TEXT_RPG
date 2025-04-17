using System.Security.Cryptography.X509Certificates;

namespace TEXT_RPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameLogic gameLogic = new GameLogic();
            gameLogic.StartGame();

        }
        class GameLogic
        {
            List<Item> InventoryList = new List<Item>();
            List<Item> itemList = new List<Item>();
            public void InventoryPrint()
            {
                foreach (var item in InventoryList)
                {
                    if (item.Type == "무기")
                    {
                        if (item.IsEquipped)
                        {
                            Console.WriteLine($"{InventoryList.IndexOf(item) + 1}.{item.Name} | 공격력 +{item.Power} | {item.Info}[E]");
                        }
                        else if (!item.IsEquipped)
                        {
                            Console.WriteLine($"{InventoryList.IndexOf(item) + 1}.{item.Name} | 공격력 +{item.Power} | {item.Info}");
                        }
                    }
                    else if (item.Type == "방어구")
                    {
                        if (item.IsEquipped)
                        {
                            Console.WriteLine($"{InventoryList.IndexOf(item) + 1}.{item.Name} | 방어력 +{item.Power} | {item.Info}[E]");
                        }
                        else if (!item.IsEquipped)
                        {
                            Console.WriteLine($"{InventoryList.IndexOf(item) + 1}.{item.Name} | 방어력 +{item.Power} | {item.Info}");
                        }
                    }

                }
            }
            
            int Action = 0;
            Job job = new Job();
            bool SceneMove = false;
            Player player = new Player();
            public void StartGame()
            {
                Start();
            }

            void Start()
            {
                ItemList();
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\n이름을 설정해주세요.");
                Console.Write(">>");
                player.Name = Console.ReadLine();//이름받기
                while (string.IsNullOrEmpty(player.Name))
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(500);
                    Console.Clear();
                    Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\n이름을 설정해주세요.");
                    Console.Write(">>");
                    player.Name = Console.ReadLine();
                }
                Console.WriteLine($"안녕하세요 {player.Name}님\n직업을 선택해주세요.");
                foreach (var item in Enum.GetValues(typeof(Job)))//직업 선택
                {
                    Console.WriteLine($"{(int)item}.{item}");
                }
                Console.Write("직업을 골라주세요.\n>>");
                while (!int.TryParse(Console.ReadLine(), out Action) && 1 <= Action && Action >= 3)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(500);
                    Console.Clear();
                    Console.WriteLine($"안녕하세요 {player.Name}님\n직업을 선택해주세요.");
                    foreach (var item in Enum.GetValues(typeof(Job)))//직업 선택
                    {
                        Console.WriteLine($"{(int)item}.{item}");
                    }
                    Console.Write("직업을 골라주세요.\n>>");
                }//직업 선택
                player.Job = Enum.GetName(typeof(Job), Action);
                Village();

            }
            void Village()//마을
            {
                while (true)
                {
                    Console.WriteLine($"스파르타 마을에 오신것을 환영합니다.{player.Name}님\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
                    foreach (var item in Enum.GetValues(typeof(VillageActiveList)))//마을에서 활동가능한 목록 출력
                    {
                        Console.WriteLine($"{(int)item}.{item}");
                    }
                    Console.Write(">>");
                    while (!int.TryParse(Console.ReadLine(), out Action) && 1 <= Action && Action >= 4)
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        Thread.Sleep(500);
                        Console.Clear();
                        Console.WriteLine($"스파르타 마을에 오신것을 환영합니다.{player.Name}님\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
                        foreach (var item in Enum.GetValues(typeof(VillageActiveList)))//마을에서 활동가능한 목록 출력
                        {
                            Console.WriteLine($"{(int)item}.{item}");
                        }
                        Console.Write(">>");
                    }//마을에서 활동가능한 목록 출력
                    switch (Action)
                    {
                        case 1:
                            Console.Clear();
                            Status();
                            break;
                        case 2:
                            Console.Clear();
                            Inventory();
                            break;
                        case 3:
                            Console.Clear();
                            Shop();
                            break;
                        case 4:
                            Console.Clear();
                            Dungeon();
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            Thread.Sleep(500);
                            Console.Clear();
                            break;
                    }
                }


            }
            void Status()
            {
                while (true)
                {
                    Console.WriteLine("상태 보기\n캐릭터의 정보가 표시됩니다.\n\n");
                    Console.WriteLine($"레벨 : {player.Level}\n{player.Name}({player.Job})\n공격력 : {player.Attack}\n방어력 : {player.Defense}\n체 력 : {player.Health}\nGold : {player.Gold}G");
                    Console.WriteLine("\n\n0.나가기");
                    Console.Write(">>");
                    while (!int.TryParse(Console.ReadLine(), out Action) && Action == 0)
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        Thread.Sleep(500);
                        Console.Clear();
                        Console.WriteLine("상태 보기\n캐릭터의 정보가 표시됩니다.\n\n");
                        Console.WriteLine($"레벨 : {player.Level}\n{player.Name}({player.Job})\n공격력 : {player.Attack}\n방어력 : {player.Defense}\n체 력 : {player.Health}\nGold : {player.Gold}G");
                        Console.WriteLine("\n\n0.나가기");
                        Console.Write(">>");
                    }
                    Console.Clear();
                    return;
                }


            }
            void Inventory()
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("인벤토리 - 장착 관리\n보유 중인 아이템을 관리할 수 있습니다.");
                    Console.WriteLine("\n[아이템 목록]");
                    InventoryPrint();
                    Console.Write("장비를 장착,해제하려면 숫자를 눌러주세요\n0.나가기\n>>");
                    while (!int.TryParse(Console.ReadLine(), out Action) && 0 <= Action && Action >= InventoryList.Count)
                    {
                        Console.WriteLine("인벤토리 - 장착 관리\n보유 중인 아이템을 관리할 수 있습니다.");
                        Console.WriteLine("\n[아이템 목록]");
                        InventoryPrint();
                        Console.Write("장비를 장착,해제하려면 숫자를 눌러주세요\n0.나가기\n>>");
                    }
                    if (Action == 0)
                    {
                        Console.Clear();
                        return;
                    }
                    else if (1 <= Action && Action <= InventoryList.Count)
                    {
                        if (InventoryList[Action - 1].IsEquipped)
                        {
                            InventoryList[Action - 1].IsEquipped = false;
                        }
                        else if (!InventoryList[Action - 1].IsEquipped)
                        {
                            InventoryList[Action - 1].IsEquipped = true;
                        }

                    }
                }

            }
            public void Shop()
            {
                while (true)
                {

                    Console.WriteLine("상점\n필요한 아이템을 얻을 수 있는 상점입니다.");
                    Console.WriteLine($"\n[보유골드]\n{player.Gold}G");
                    ItemListPrint();
                    Console.Write("\n\n아이템을 구매하시려면 아이템의 번호를 입력해주세요.\n0.나가기\n>>");
                    while (!int.TryParse(Console.ReadLine(), out Action) && 0 <= Action && Action >= itemList.Count)
                    {
                        Console.Clear();
                        Console.WriteLine("상점\n필요한 아이템을 얻을 수 있는 상점입니다.");
                        Console.WriteLine($"\n[보유골드]\n{player.Gold}G");
                        ItemListPrint();
                        Console.Write("\n\n아이템을 구매하시려면 아이템의 번호를 입력해주세요.\n0.나가기\n>>");
                        //상점 정보
                    }
                    Console.Clear();
                    if (Action == 0)
                    {
                        Console.Clear();
                        return;
                    }
                    else if (1 <= Action && Action <= itemList.Count)
                    {
                        Console.Clear();
                        itemList[Action - 1].IsBuy = true;
                        player.Gold -= itemList[Action - 1].Price;
                        InventoryList.Add(itemList[Action - 1]);
                        
                        

                        //아이템 구매
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        Thread.Sleep(500);
                        Console.Clear();
                    }
                }
                void ItemListPrint()
                {
                    foreach (var item in itemList)
                    {
                        if (item.Type == "무기")
                        {
                            if (item.IsBuy)
                            {
                                Console.WriteLine($"{itemList.IndexOf(item) + 1}.[품절] {item.Name} | 공격력 +{item.Power} | {item.Info}");
                            }
                            else if (!item.IsBuy)
                            {
                                Console.WriteLine($"{itemList.IndexOf(item) + 1}.       {item.Name} | 공격력 +{item.Power} | {item.Info}");
                            }

                        }
                        else if (item.Type == "방어구")
                        {
                            if (item.IsBuy)
                            {
                                Console.WriteLine($"{itemList.IndexOf(item) + 1}.[품절] {item.Name} | 방어력 +{item.Power} | {item.Info}");
                            }
                            else if (!item.IsBuy)
                            {
                                Console.WriteLine($"{itemList.IndexOf(item) + 1}.       {item.Name} | 방어력 +{item.Power} | {item.Info}");
                            }
                        }

                    }
                }

            }
            void Dungeon()
            {
                while (true)
                {
                    Console.WriteLine("던전 선택\n던전의 정보가 표시됩니다.");
                    Console.Write("1.던전 선택\n0.마을로 가기\n>>");
                    while (!int.TryParse(Console.ReadLine(), out Action) && 0 <= Action && Action >= 1)
                    {
                        //던전 정보
                    }
                    Console.Clear();
                    return;
                }

            }



            class Player
            {
                public string Job { get; set; }
                public string Name { get; set; }
                public int Health { get; set; } = 100;
                public int Attack { get; set; } = 10;
                public int Defense { get; set; } = 5;
                public int Level { get; set; } = 1;
                public int Gold { get; set; } = 1500;
            }

            class Item
            {
                public string Name { get; set; }
                public string Info { get; set; }
                public string Type { get; set; }
                public int ItemID { get; set; }
                public int Price { get; set; }
                public int Power { get; set; }
                public bool IsEquipped { get; set; } = false;
                public bool IsBuy { get; set; } = false;
                public bool IsUsable { get; set; } = false;
                public Item(string name, string info, string type, int price, int power, int itemID)
                {
                    Name = name;
                    Info = info;
                    Type = type;
                    Price = price;
                    Power = power;
                    ItemID = itemID;
                }
            }

            public void ItemList()
            { 
                itemList.Add(new Item("나무검     ", "      나무로 만들어진 검    ", "무기", 100, 10, 1));
                itemList.Add(new Item("나무 방패  ", "      나무로 만들어진 방패  ", "방어구", 100, 5, 2));
                itemList.Add(new Item("나무 활    ", "      나무로 만들어진 활    ", "무기", 100, 8, 3));
                itemList.Add(new Item("마법 지팡이", "마법을 사용할 수 있는 지팡이", "무기", 100, 15, 4));
                itemList.Add(new Item("천옷       ", "      허접하게만들어졌다.   ", "방어구", 100, 4, 5));
                itemList.Add(new Item("낡은 신발  ", "      오래되어 낡아있다.    ", "방어구", 100, 3, 6));

            }
            enum Job
            {
                Warrior = 1,
                Mage,
                Archer,
            }
            enum VillageActiveList
            {
                Status = 1,
                Inventory,
                Shop,
                Dungeon,
            }

        }


    }
}
