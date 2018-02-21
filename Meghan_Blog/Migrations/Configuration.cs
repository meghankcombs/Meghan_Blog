using Meghan_Blog.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Configuration;

namespace Meghan_Blog.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Meghan_Blog.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            #region Example Code
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            #endregion

            #region Role Manager
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            //ADMIN
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            //MODERATOR
            if (!context.Roles.Any(r => r.Name == "Moderator"))
            {
                roleManager.Create(new IdentityRole { Name = "Moderator" });
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //ADMIN
            if (!context.Users.Any(u => u.Email == "meghankcombs@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "meghankcombs@gmail.com",
                    Email = "meghankcombs@gmail.com",
                    FirstName = "Meghan",
                    LastName = "Combs",
                    DisplayName = "MCDev",
                    UserPhoto = new byte[0]
                }, "Boo&Grace22");
            }

            var userId = userManager.FindByEmail("meghankcombs@gmail.com").Id;
            userManager.AddToRole(userId, "Admin");

            //MODERATOR
            if (!context.Users.Any(u => u.Email == "cadinnc@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "cadinnc@gmail.com",
                    Email = "cadinnc@gmail.com",
                    FirstName = "Carol",
                    LastName = "Dernbach",
                    DisplayName = "CAD",
                    UserPhoto = new byte[0]
                }, "Boo&Grace22");
            }

            var userId2 = userManager.FindByEmail("cadinnc@gmail.com").Id;
            userManager.AddToRole(userId2, "Moderator");

            #endregion

            #region Seeded BlogPosts & Comments

            //context.BlogPosts.AddOrUpdate(
            //      p => p.Title,
            //      new BlogPost { Id = 10010, Title = "", Slug = "", MediaUrl = "", Created = DateTime.Now, Published = true, Abstract = "", Body = "." },
            //      new BlogPost { Id = 10020, Title = "Second Blog Post", Slug = "Second-Blog-Post", MediaUrl = "/MyImages/Financial Portal on computer.jpg", Created = DateTime.Now, Published = true, Abstract = "Second Blog Post Abstract", Body = "Boggarts lavender robes, Hermione Granger Fantastic Beasts and Where to Find Them. Bee in your bonnet Hand of Glory elder wand, spectacles House Cup Bertie Bott’s Every Flavor Beans Impedimenta. Stunning spells tap-dancing spider Slytherin’s Heir mewing kittens Remus Lupin. Palominos scarlet train black robes, Metamorphimagus Niffler dead easy second bedroom. Padma and Parvati Sorting Hat Minister of Magic blue turban remember my last." },
            //      new BlogPost { Id = 10030, Title = "Third Blog Post", Slug = "Third-Blog-Post", MediaUrl = "/MyImages/Notebook and Computer.jpeg", Created = DateTime.Now, Published = true, Abstract = "Third Blog Post Abstract", Body = "Red hair crookshanks bludger Marauder’s Map Prongs sunshine daisies butter mellow Ludo Bagman. Beaters gobbledegook N.E.W.T., Honeydukes eriseD inferi Wormtail. Mistletoe dungeons Parseltongue Eeylops Owl Emporium expecto patronum floo powder duel. Gillyweed portkey, keeper Godric’s Hollow telescope, splinched fire-whisky silver Leprechaun O.W.L. stroke the spine. Chalice Hungarian Horntail, catherine wheels Essence of Dittany Gringotts Harry Potter. Prophecies Yaxley green eyes Remembrall horcrux hand of the servant. Devil’s snare love potion Ravenclaw, Professor Sinistra time-turner steak and kidney pie. Cabbage Daily Prophet letters from no one Dervish and Banges leg." },
            //      new BlogPost { Id = 10040, Title = "Fourth Blog Post", Slug = "Fourth-Blog-Post", MediaUrl = "/MyImages/Woman on computer.jpeg", Created = DateTime.Now, Published = true, Abstract = "Fourth Blog Post Abstract", Body = "Alohamora wand elf parchment, Wingardium Leviosa hippogriff, house dementors betrayal. Holly, Snape centaur portkey ghost Hermione spell bezoar Scabbers. Peruvian-Night-Powder werewolf, Dobby pear-tickle half-moon-glasses, Knight-Bus. Padfoot snargaluff seeker: Hagrid broomstick mischief managed. Snitch Fluffy rock-cake, 9 ¾ dress robes I must not tell lies. Mudbloods yew pumpkin juice phials Ravenclaw’s Diadem 10 galleons Thieves Downfall. Ministry-of-Magic mimubulus mimbletonia Pigwidgeon knut phoenix feather other minister Azkaban. Hedwig Daily Prophet treacle tart full-moon Ollivanders You-Know-Who cursed. Fawkes maze raw-steak Voldemort Goblin Wars snitch Forbidden forest grindylows wool socks." },
            //      new BlogPost { Id = 10050, Title = "Fifth Blog Post", Slug = "Fifth-Blog-Post", MediaUrl = "/MyImages/Woman on Bed Typing.jpg", Created = DateTime.Now, Published = true, Abstract = "Fifth Blog Post Abstract", Body = "Prefect’s bathroom Trelawney veela squashy armchairs, SPEW: Gamp’s Elemental Law of Transfiguration. Magic Nagini bezoar, Hippogriffs Headless Hunt giant squid petrified. Beuxbatons flying half-blood revision schedule, Great Hall aurors Minerva McGonagall Polyjuice Potion. Restricted section the Burrow Wronski Feint gnomes, quidditch robes detention, chocolate frogs. Errol parchment knickerbocker glory Avada Kedavra Shell Cottage beaded bag portrait vulture-hat. Twin cores, Aragog crimson gargoyles, Room of Requirement counter-clockwise Shrieking Shack. Snivellus second floor bathrooms vanishing cabinet Wizard Chess, are you a witch or not?" },
            //      new BlogPost { Id = 10060, Title = "Sixth Blog Post", Slug = "Sixth-Blog-Post", MediaUrl = "/MyImages/Woman Typing on Couch.jpeg", Created = DateTime.Now, Published = true, Abstract = "Sixth Blog Post Abstract", Body = "Boggarts lavender robes, Hermione Granger Fantastic Beasts and Where to Find Them. Bee in your bonnet Hand of Glory elder wand, spectacles House Cup Bertie Bott’s Every Flavor Beans Impedimenta. Stunning spells tap-dancing spider Slytherin’s Heir mewing kittens Remus Lupin. Palominos scarlet train black robes, Metamorphimagus Niffler dead easy second bedroom. Padma and Parvati Sorting Hat Minister of Magic blue turban remember my last." }
            //    );

            //Get AuthorId
            var authorEmail = WebConfigurationManager.AppSettings["AdminEmail"];
            var authorId = context.Users.FirstOrDefault(u => u.Email == authorEmail).Id;

            //context.Comments.AddOrUpdate(
            //    c => c.Body,
            //    new Comment { BlogPostId = 10010, Created = DateTime.Now, AuthorId = authorId, Body = "Lorem ipsum dolor amet blog locavore offal, kombucha ennui heirloom green juice pitchfork." },
            //    new Comment { BlogPostId = 10020, Created = DateTime.Now, AuthorId = authorId, Body = "Seitan messenger bag salvia food truck, jianbing lomo drinking vinegar gentrify shoreditch iceland before they sold out narwhal pop-up." },
            //    new Comment { BlogPostId = 10030, Created = DateTime.Now, AuthorId = authorId, Body = "Williamsburg activated charcoal brooklyn narwhal 3 wolf moon, tumeric freegan vegan ramps waistcoat affogato." },
            //    new Comment { BlogPostId = 10040, Created = DateTime.Now, AuthorId = authorId, Body = "Organic 90's pok pok stumptown tofu wayfarers." },
            //    new Comment { BlogPostId = 10040, Created = DateTime.Now, AuthorId = authorId, Body = "Heirloom narwhal photo booth mixtape adaptogen neutra pabst swag." },
            //    new Comment { BlogPostId = 10060, Created = DateTime.Now, AuthorId = authorId, Body = "Master cleanse semiotics edison bulb cloud bread before they sold out small batch vice cornhole twee affogato succulents 90's farm-to-table tumeric hot chicken." }
            //    );

            #endregion
        }
    }
}
