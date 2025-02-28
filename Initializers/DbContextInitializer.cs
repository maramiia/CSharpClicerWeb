using AutoMapper;
using CSharpClicker.Web.Domain;
using CSharpClicker.Web.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.Web.Initializers;

public static class DbContextInitializer
{
    public static void AddAppDbContext(IServiceCollection services)
    {
        var pathToDbFile = GetPathToDbFile();
        services
            .AddDbContext<AppDbContext>(options => options
                .UseSqlite($"Data Source={pathToDbFile}"));

        string GetPathToDbFile()
        {
            var applicationFolder = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData), "CSharpClicker");

            if (!Directory.Exists(applicationFolder))
            {
                Directory.CreateDirectory(applicationFolder);
            }

            return Path.Combine(applicationFolder, "CSharpClicker.db");
        }
    }

    public static void InitializeDbContext(AppDbContext appDbContext)
    {




        const string Weapon1 = "Утигатана";
        const string Weapon2 = "Меч рыцаря Лотрика";
        const string Weapon3 = "Меч убийцы нежити";
        const string Weapon4 = "Скимитары наемника";
        const string Weapon5 = "Иритилльский меч";

        const string Armor1 = "Доспех Солнца";
        const string Armor2 = "Броня Падшего рыцаря";
        const string Armor3 = "Сет Легиона нежити";
        const string Armor4 = "Сет Волчьего рыцаря";
        const string Armor5 = "Железный сет драконоборца";

        const string Boss1 = "Понтифик Саливан";
        const string Boss2 = "Олдрик Пожиратель Богов";
        const string Boss3 = "Танцовщица Холодной Долины";
        const string Boss4 = "Душа Пепла";
        const string Boss5 = "Отец Ариандель и сестра Фриде";

        const string Boost1 = "Хранительница огня";
        const string Boost2 = "Неразрывный Лоскутик";
        const string Boost3 = "Леонхард Безымянный палец";
        const string Boost4 = "Рыцарь-раб Гаэль";
        const string Boost5 = "Черный Кузнец Андре";

        appDbContext.Database.Migrate();

        var existingBoosts = appDbContext.Boosts.ToArray();

        var existingBoss = appDbContext.Bosses.ToArray();
        var existingArmor = appDbContext.Armors.ToArray();
        var existingWeapon = appDbContext.Weapons.ToArray();

        AddWeaponIfNotExist(Weapon1, price: 15, damage: 10);
        AddWeaponIfNotExist(Weapon2, price: 17, damage: 50);
        AddWeaponIfNotExist(Weapon3, price: 10000, damage: 100);
        AddWeaponIfNotExist(Weapon4, price: 30000, damage: 500);
        AddWeaponIfNotExist(Weapon5, price: 50000, damage: 2000);

        AddArmorIfNotExist(Armor1, price: 10, profit: 10);
        AddArmorIfNotExist(Armor2, price: 15, profit: 100);
        AddArmorIfNotExist(Armor3, price: 46000, profit: 200);
        AddArmorIfNotExist(Armor4, price: 60000, profit: 500);
        AddArmorIfNotExist(Armor5, price: 100000, profit: 10000);

        AddBossIfNotExist(Boss1, health: 50, reward: 50000);
        AddBossIfNotExist(Boss2, health: 200000, reward: 100000);
        AddBossIfNotExist(Boss3, health: 400000, reward: 200000);
        AddBossIfNotExist(Boss4, health: 700000, reward: 500000);
        AddBossIfNotExist(Boss5, health: 1000000, reward: 5000000);

        AddBoostIfNotExist(Boost2, price: 10, profit: 1);
        AddBoostIfNotExist(Boost5, price: 15, profit: 20);
        AddBoostIfNotExist(Boost3, price: 2000, profit: 500, isAuto: true);
        AddBoostIfNotExist(Boost4, price: 5000, profit: 1000);
        AddBoostIfNotExist(Boost1, price: 100000, profit: 5000, isAuto: true);

        appDbContext.SaveChanges();


        void AddWeaponIfNotExist(string name, long price, long damage)
        {
            if (!existingWeapon.Any(eb => eb.Title == name))
            {
                var pathToImage = Path.Combine(".", "Resources", "WeaponImages", $"{name}.png");
                using var fileStream = File.OpenRead(pathToImage);
                using var memoryStream = new MemoryStream();

                fileStream.CopyTo(memoryStream);

                appDbContext.Weapons.Add(new Weapon
                {
                    Title = name,
                    Image = memoryStream.ToArray(),
                    Price = price,
                    Damage = damage,
                });
            }
        }


        void AddArmorIfNotExist(string name, long price, long profit)
        {
            if (!existingArmor.Any(eb => eb.Title == name))
            {
                var pathToImage = Path.Combine(".", "Resources", "ArmorImages", $"{name}.jpg");
                using var fileStream = File.OpenRead(pathToImage);
                using var memoryStream = new MemoryStream();

                fileStream.CopyTo(memoryStream);

                appDbContext.Armors.Add(new Armor
                {
                    Title = name,
                    Image = memoryStream.ToArray(),
                    Price = price,
                    Profit = profit
                });
            }
        }


        void AddBossIfNotExist(string name, long health, long reward)
        {
            if (!existingBoss.Any(eb => eb.Title == name))
            {
                var pathToImage = Path.Combine(".", "Resources", "BossImages", $"{name}.jpg");
                using var fileStream = File.OpenRead(pathToImage);
                using var memoryStream = new MemoryStream();

                fileStream.CopyTo(memoryStream);

                appDbContext.Bosses.Add(new Boss
                {
                    Title = name,
                    Price = health,
                    Reward = reward,
                    Image = memoryStream.ToArray(),
                });

            }

        }

        void AddBoostIfNotExist(string name, long price, long profit, bool isAuto = false)
        {
            if (!existingBoosts.Any(eb => eb.Title == name))
            {
                var pathToImage = Path.Combine(".", "Resources", "BoostImages", $"{name}.jpg");
                using var fileStream = File.OpenRead(pathToImage);
                using var memoryStream = new MemoryStream();

                fileStream.CopyTo(memoryStream);

                appDbContext.Boosts.Add(new Boost
                {
                    Title = name,
                    Price = price,
                    Profit = profit,
                    IsAuto = isAuto,
                    Image = memoryStream.ToArray(),
                });
            }
        }
    }
}
