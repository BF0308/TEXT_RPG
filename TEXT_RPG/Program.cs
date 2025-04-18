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
            public void InventoryPrint()//인벤토리 리스트 출력
            {
                foreach (var item in InventoryList)
                {
                    if (item.Type == "무기")//아이템 종류 확인
                    {
                        if (item.IsEquipped)//장착여부
                        {
                            Console.WriteLine($"{InventoryList.IndexOf(item) + 1}.{item.Name} | 공격력 +{item.Power} | {item.Info}[E]");
                        }
                        else if (!item.IsEquipped)
                        {
                            Console.WriteLine($"{InventoryList.IndexOf(item) + 1}.{item.Name} | 공격력 +{item.Power} | {item.Info}");
                        }
                    }
                    else if (item.Type == "방어구")//아이템 종류 확인
                    {
                        if (item.IsEquipped)//장착여부
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
            public void StartGame()//게임의 시작부분(초기값)
            {
                Start();
            }

            void Start()//시작하면 가장먼저 실행되는부분
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
                Console.Clear();
                Console.WriteLine($"안녕하세요 {player.Name}님\n직업을 선택해주세요.");
                foreach (var item in Enum.GetValues(typeof(Job)))//직업 선택
                {
                    Console.WriteLine($"{(int)item}.{item}");
                }
                Console.Write("직업을 골라주세요.\n>>");
                while (!int.TryParse(Console.ReadLine(), out Action) || 1 > Action || Action > 3)
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
                    Console.Clear();
                    Console.WriteLine($"스파르타 마을에 오신것을 환영합니다.{player.Name}님\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
                    foreach (var item in Enum.GetValues(typeof(VillageActiveList)))//마을에서 활동가능한 목록 출력
                    {
                        Console.WriteLine($"{(int)item}.{item}");
                    }
                    Console.Write(">>");
                    while (!int.TryParse(Console.ReadLine(), out Action) || 1 > Action || Action > 5)
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
                    }
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
                        case 5:
                            Console.Clear();
                            Rest();
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            Thread.Sleep(500);
                            Console.Clear();
                            break;
                    }
                }


            }
            void Status()//상태창
            {
                while (true)
                {
                    Console.WriteLine("상태 보기\n캐릭터의 정보가 표시됩니다.\n\n");
                    Console.WriteLine($"레벨 : {player.Level}\n{player.Name}({player.Job})\n공격력 : {player.Attack}\n방어력 : {player.Defense}\n체 력 : {player.Health}\nGold : {player.Gold}G");
                    Console.WriteLine("\n\n0.나가기");
                    Console.Write(">>");
                    while (!int.TryParse(Console.ReadLine(), out Action) || Action != 0)
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
            void Inventory()//인벤토리
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("인벤토리 - 장착 관리\n보유 중인 아이템을 관리할 수 있습니다.");
                    Console.WriteLine("\n[아이템 목록]");
                    if (InventoryList.Count == 0)//아이템이 없으면 출력
                    {
                        Console.WriteLine("\n보유하신 아이템이 없습니다.\n상점에서 구매하거나, 추후에 나올 던전에서 찾아보세요!\n");
                    }
                    InventoryPrint();
                    Console.Write("장비를 장착,해제하려면 숫자를 눌러주세요\n0.나가기\n>>");
                    while (!int.TryParse(Console.ReadLine(), out Action) || 0 > Action || Action > InventoryList.Count)
                    {
                        Console.Clear();
                        Console.WriteLine("인벤토리 - 장착 관리\n보유 중인 아이템을 관리할 수 있습니다.");
                        Console.WriteLine("\n[아이템 목록]");
                        if (InventoryList.Count == 0)//아이템이 없으면 출력
                        {
                            Console.WriteLine("\n보유하신 아이템이 없습니다.\n상점에서 구매하거나, 추후에 나올 던전에서 찾아보세요!\n");
                        }
                        else//아이템이 있으면 리스트출력
                        {
                            InventoryPrint();
                        }
                        Console.Write("장비를 장착,해제하려면 숫자를 눌러주세요\n0.나가기\n>>");
                    }
                    if (Action == 0)
                    {
                        Console.Clear();
                        return;
                    }
                    else if (1 <= Action && Action <= InventoryList.Count)//아이템 범위
                    {
                        inventorySwap(Action);
                    }

                }
                void inventorySwap(int Action)//아이템 장착/해제
                {
                    if (InventoryList[Action - 1].IsEquipped)
                    {
                        InventoryList[Action - 1].IsEquipped = false;
                    }
                    else if (!InventoryList[Action - 1].IsEquipped)
                    {
                        // 현재 장착된 무기가 있는지 확인
                        if (InventoryList.Any(item => item.Type == "무기" && item.IsEquipped))
                        {
                            // 이미 장착된 무기를 찾아서 해제
                            var equippedWeapon = InventoryList.First(item => item.Type == "무기" && item.IsEquipped);
                            Console.WriteLine($"{equippedWeapon.Name}이(가) 해제되었습니다.");
                            equippedWeapon.IsEquipped = false;
                        }
                        if (InventoryList.Any(item => item.Type == "방어구" && item.IsEquipped))
                        {
                            // 이미 장착된 무기를 찾아서 해제
                            var equippedWeapon = InventoryList.First(item => item.Type == "방어구" && item.IsEquipped);
                            Console.WriteLine($"{equippedWeapon.Name}이(가) 해제되었습니다.");
                            equippedWeapon.IsEquipped = false;
                        }

                        // 선택한 아이템을 장착 또는 해제
                        if (InventoryList[Action - 1].IsEquipped)
                        {
                            Console.WriteLine($"{InventoryList[Action - 1].Name}이(가) 해제되었습니다.");
                            InventoryList[Action - 1].IsEquipped = false;
                        }
                        else
                        {
                            Console.WriteLine($"{InventoryList[Action - 1].Name}이(가) 장착되었습니다.");
                            InventoryList[Action - 1].IsEquipped = true;
                        }
                    }
                    




                }
            }
            public void Shop()//상점
            {
                while (true)
                {

                    Console.WriteLine("상점\n필요한 아이템을 얻을 수 있는 상점입니다.");//구매
                    Console.WriteLine($"\n[보유골드]\n{player.Gold}G");
                    ItemListPrint();
                    Console.Write("\n\n1.아이템 구매\n2.아이템 판매\n0.나가기\n>>");
                    while (!int.TryParse(Console.ReadLine(), out Action) || 0 > Action || Action > itemList.Count)//리스트 이외의 값은 다시받음.
                    {
                        Console.Clear();
                        Console.WriteLine("상점\n필요한 아이템을 얻을 수 있는 상점입니다.");
                        Console.WriteLine($"\n[보유골드]\n{player.Gold}G");
                        ItemListPrint();
                        Console.Write("\n\n1.아이템 구매\n2.아이템 판매\n0.나가기\n>>");
                        //상점 정보
                    }
                    Console.Clear();
                    if (Action == 0)
                    {
                        Console.Clear();
                        return;
                    }
                    else if (1 <= Action && Action <= 2)
                    {
                        if (Action == 1)
                        {
                            BuyShop();
                        }
                        else if (Action == 2)
                        {
                            SellShop();
                        }
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        Thread.Sleep(500);
                        Console.Clear();
                    }
                }
                void ItemListPrint()//상점에 보여줄 리스트
                {
                    foreach (var item in itemList)
                    {
                        if (item.Type == "무기")
                        {
                            if (item.IsBuy)
                            {
                                Console.WriteLine($"{itemList.IndexOf(item) + 1}.{item.Name} | 공격력 +{item.Power} | {item.Info} | 구매 완료");
                            }
                            else if (!item.IsBuy)
                            {
                                Console.WriteLine($"{itemList.IndexOf(item) + 1}.{item.Name} | 공격력 +{item.Power} | {item.Info} | {item.Price}G");
                            }

                        }
                        else if (item.Type == "방어구")
                        {
                            if (item.IsBuy)
                            {
                                Console.WriteLine($"{itemList.IndexOf(item) + 1}.{item.Name} | 방어력 +{item.Power} | {item.Info} | 구매 완료");
                            }
                            else if (!item.IsBuy)
                            {
                                Console.WriteLine($"{itemList.IndexOf(item) + 1}.{item.Name} | 방어력 +{item.Power} | {item.Info} | {item.Price}G");
                            }
                        }

                    }
                }
                void BuyShop()//구매상점
                {
                    while (true)
                    {
                        Console.WriteLine("상점-구매\n필요한 아이템을 얻을 수 있는 상점입니다.");//구매
                        Console.WriteLine($"\n[보유골드]\n{player.Gold}G");
                        ItemListPrint();
                        Console.Write("\n\n아이템을 구매하시려면 아이템의 번호를 입력해주세요.\n0.나가기\n>>");
                        while (!int.TryParse(Console.ReadLine(), out Action) || 0 > Action || Action > itemList.Count)//리스트 이외의 값은 다시받음.
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
                        else if (1 <= Action && Action <= itemList.Count)//아이템 범위지정
                        {
                            if (itemList[Action - 1].IsBuy)//구매여부확인
                            {
                                Console.WriteLine("품절된 아이템입니다.");
                            }
                            else if (!itemList[Action - 1].IsBuy)//구매여부확인
                            {
                                if (player.Gold < itemList[Action - 1].Price)//골드 확인
                                {
                                    Console.WriteLine("Gold가 부족합니다.");
                                    Thread.Sleep(500);
                                    continue;
                                }
                                else if (player.Gold >= itemList[Action - 1].Price)//골드확인
                                {
                                    player.Gold -= itemList[Action - 1].Price;
                                    itemList[Action - 1].IsBuy = true;
                                    InventoryList.Add(itemList[Action - 1]);
                                }
                                Console.Clear();
                            }
                            else
                            {
                                Console.WriteLine("잘못된 입력입니다.");
                                Thread.Sleep(500);
                                Console.Clear();
                            }

                        }
                    }

                }
                void SellShop()//판매상점
                {
                    while (true)
                    {
                        Console.WriteLine("상점-판매\n필요한 아이템을 얻을 수 있는 상점입니다.\n판매금은 원가의 85%입니다.");//판매
                        Console.WriteLine($"\n[보유골드]\n{player.Gold}G");
                        InventoryPrint();
                        Console.Write("\n\n아이템을 판매하시려면 아이템의 번호를 입력해주세요.\n0.나가기\n>>");
                        while (!int.TryParse(Console.ReadLine(), out Action) || 0 > Action || Action > InventoryList.Count)//리스트 이외의 값은 다시받음.
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                            Console.Clear();
                            Console.WriteLine("상점\n필요한 아이템을 얻을 수 있는 상점입니다.\n판매금은 원가의 85%입니다.");//판매
                            Console.WriteLine($"\n[보유골드]\n{player.Gold}G");
                            InventoryPrint();
                            Console.Write("\n\n아이템을 판매하시려면 아이템의 번호를 입력해주세요.\n0.나가기\n>>");
                            //상점 정보
                        }
                        Console.Clear();
                        if (Action == 0)
                        {
                            Console.Clear();
                            return;
                        }
                        else if (1 <= Action && Action <= InventoryList.Count)//아이템 범위지정
                        {
                            Console.WriteLine($"{InventoryList[Action - 1].Name}을(를) 판매하셨습니다.");
                            player.Gold += (InventoryList[Action - 1].Price * 85) / 100;
                            itemList[InventoryList[Action - 1].ItemID].IsBuy = false;
                            InventoryList.Remove(InventoryList[Action - 1]);
                            Thread.Sleep(500);
                            Console.Clear();
                        }
                    }

                }

            }
            void Dungeon()//나중에 추가할 던전
            {
                while (true)
                {
                    Console.WriteLine("던전 선택\n던전의 정보가 표시됩니다.(업데이트 예정!)");
                    Console.Write("1.던전 선택\n0.마을로 가기\n>>");
                    while (!int.TryParse(Console.ReadLine(), out Action) || 0 > Action || Action > 1)
                    {
                        //던전 정보
                    }
                    Console.Clear();
                    return;
                }

            }
            void Rest()//휴식
            {
                if (player.Health < 100)
                {
                    Console.WriteLine("충분한 휴식으로 체력이 전부회복되었습니다.");
                    player.Health = 100;
                }
                else if (player.Health == 100)
                {
                    Console.WriteLine("체력이 충분한 것 같다.");
                }
            }



            class Player//플레이어 기본틀
            {
                public string Job { get; set; }
                public string Name { get; set; }
                public int Health { get; set; } = 100;
                public int Attack { get; set; } = 10;
                public int Defense { get; set; } = 5;
                public int Level { get; set; } = 1;
                public int Gold { get; set; } = 1500;
            }

            class Item//아이템 기본틀
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

            public void ItemList()//아이템 목록(상점에 적용됨)
            {
                itemList.Add(new Item("나무검     ", "나무로 만들어진 검                ", "무기", 100, 10, 0));
                itemList.Add(new Item("이빠진 도끼", "이빠진 도끼                       ", "무기", 100, 5, 1));
                itemList.Add(new Item("나무 활    ", "나무로 만들어진 활                ", "무기", 100, 8, 2));
                itemList.Add(new Item("무쇠갑옷   ", "무쇠로 만들어져 튼튼한 갑옷입니다.", "방어구", 100, 15, 3));
                itemList.Add(new Item("천옷       ", "허접하게만들어졌다.               ", "방어구", 100, 4, 4));
                itemList.Add(new Item("낡은 가죽옷", "오래되어 낡아있다.                ", "방어구", 100, 3, 5));

            }
            enum Job//직업목록
            {
                Warrior = 1,
                Mage,
                Archer,
            }
            enum VillageActiveList//마을에서 할수있는 활동목록
            {
                Status = 1,
                Inventory,
                Shop,
                Dungeon,
                Rest
            }

        }


    }
}
