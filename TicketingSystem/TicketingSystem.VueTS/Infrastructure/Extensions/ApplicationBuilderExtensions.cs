using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem.Data;
using TicketingSystem.Services;
using TicketingSystem.VueTS.Common.Constants;
using DATA_ENUMS = TicketingSystem.Data.Enums;
using DATA_MODELS = TicketingSystem.Data.Models;
using IdentityRole = TicketingSystem.Services.IdentityRole;

namespace TicketingSystem.VueTS.Infrastructure.Extensions
{
	public static class ApplicationBuilderExtensions
	{
		public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
		{
			using (IServiceScope serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
			{
				var db = serviceScope.ServiceProvider.GetService<TicketingSystemDbContext>();
				db.Database.Migrate();


				IUserService userManager = serviceScope.ServiceProvider.GetService<IUserService>();
				IRoleService roleManager = serviceScope.ServiceProvider.GetService<IRoleService>();

				Task.Run(async () =>
				{
					await SeedDatabase(db, userManager);

					string adminName = WebConstants.AdministratorRole;

					string[] roles = new[]
					{
						adminName,
						WebConstants.ClientRole,
						WebConstants.SuportRole
					};

					foreach (var role in roles)
					{
						bool roleExists = await roleManager.RoleExistsAsync(role);

						if (!roleExists)
						{
							await roleManager.CreateAsync(new IdentityRole
							{
								Name = role
							});
						}
					}

					string adminEmail = "admin@mysite.com";

					User adminUser = await userManager.FindByEmailAsync(adminEmail);

					if (adminUser == null)
					{
						adminUser = new User
						{
							Email = adminEmail,
							UserName = adminName,
							Name = adminName,
							IsApproved = true
						};

						await userManager.CreateAsync(adminUser, "admin");

						var admin = db.Users.Where(u => u.UserName == "Administrator")
						.Select(u => new User
						{
							Id = u.Id,
							UserName = u.UserName,
							Name = u.Name,
							Email = u.Email,
							IsApproved = u.IsApproved,
							SecurityStamp = u.SecurityStamp
						})
						.FirstOrDefault();

						await userManager.AddToRoleAsync(admin, adminName);
					}
				})
				.Wait();
			}

			return app;
		}

		private static async Task SeedDatabase(TicketingSystemDbContext database, IUserService userManager)
		{
			if (!database.Projects.Any())
			{
				SeedProjects(database);
			}

			if (!database.Users.Any())
			{
				await SeedUsers(database, userManager);
			}

			if (!database.Tickets.Any())
			{
				SeedTickets(database);
			}

			//if (!database.Messages.Any())
			//{
			//    SeedMessages(database);
			//}
		}

		private static void SeedProjects(TicketingSystemDbContext database)
		{
			var projectOne = new DATA_MODELS.Project
			{

				Name = "Craft A Minecraft Creeper Robot",
				Description = "When I wrote my new book Make: Minecraft for Makers, " +
				"you know I had to include a monster Creeper project. Here’s how you can build a motorized Creeper," +
				" with a metal skeleton and wooden skin. Aside from the fact that this thing most certainly doesn’t blow up," +
				" you’ll love it, and you’ll learn a lot about robotics and Arduino along the way. Let’s get to it!The Creeper consists of a robot chassis kit with" +
				" add - on parts creating the mob’s distinctive armless body,with a servo motor to move the head around.Begin by taking a look at the Creeper in-game." +
				"Just be sure to stick to Creative mode or you may find yourself getting blown up!"
			};

			var projectTwo = new DATA_MODELS.Project
			{
				Name = "Light Up Leather Arm Braces",
				Description = "Wearable microcontrollers have found their way into cosplay, fashion, and daily wear but are usually textile based." +
				" In this Skill Builder we’ll explore techniques for including leather in your wearables by examining my forearm bracer project," +
				" which uses a built-in Adafruit Gemma and RGB NeoPixels that change color patterns with a simple capacitive touch of the medallion" +
				" on its side. Read along and learn some key tips to working with leather and electronics, both on their own, and combined into one" +
				" project.Leather is a favorite material of crafters,cosplayers,and makers for good reasons. It is moldable, cuttable, colorable," +
				" and durable."
			};

			var projectThree = new DATA_MODELS.Project
			{
				Name = "Edge-Lit LED Signs",
				Description = "Two things that most any hackerspace can benefit from are better signage explaining the space," +
				" and festive LED decorations.In preparation for Noisebridge’s 10 - year anniversary in 2017 we decided to address both in one project;" +
				" creating suspended LED signs that identify the work areas of our space in San Francisco.This type of signage is not uncommon," +
				" but our extensive installation and our means of keeping costs down led to several requests for a tutorial. You can read more about it at the noisebridge" +
				" blog, but here’s a quick overview of how we made and installed our 14 suspended, edge - lit LED signs."
			};

			var projectFour = new DATA_MODELS.Project
			{
				Name = "Custom Canine Wheelchair",
				Description = "A friend needed a wheelchair for their French Bulldog at short notice. They were unable to afford the costly commercial wheelchairs available online." +
				" I rose to the occasion to design something for an adorable dog, and the results were fantastic. Murray loved her chair!I’ve since built two other chairs for dogs," +
				" and I’ve worked on iterating on what I call the “FiGO” design and documentation to encourage dog" +
				" owners to tackle this project for their pet in need.This device uses parametrically designed 3D printed joint pieces that fit into acrylic or aluminum tubing.The tubing" +
				" can be easily customized to the dog for both fit and style, and the 3D printed pieces can also be personalized via the Customizer application on Thingiverse.Currently screw size, tube outer diameter," +
				" wheel angle, and your dog’s measurements can be input to view a rendering of your dog’s custom wheelchair."
			};

			var projectFive = new DATA_MODELS.Project
			{
				Name = "Near-Field Fingernails",
				Description = "Nail art has seen some advances in the past decades, from the invention of cellulose-based polish 100 years ago, through thin plastic false nails, to the available-everywhere acrylic sculpted nails." +
				" But it was about time nails went techno.On the security conference circuit,Baybe Doll(aka Emily Mitchell) had been getting her nail technician to embed small devices with readable data into her acrylic nails." +
				"However,the technology wasn’t available to the masses,and when it was,it was big and chunky.NFC(near - field communication) tags are the solution; they’re tiny and they’re powered by nearby magnetic fields so they don’t need batteries." +
				"The first NFC tag I tried to put on my nail was an NXP Mifare Ultralight C NTAG213 that I bought from a supplier to retail stores(it’s the thing that makes your shopping go beep when you leave if it hasn’t been deactivated). " +
				"It was huge in comparison to the ones I use now."
			};

			var projectSix = new DATA_MODELS.Project
			{
				Name = "Quick Mini-Loom Ornament",
				Description = "One of the most fun and satisfying projects in my book Fabric and Fiber Inventions is the little cardboard loom. With just some scraps of board and bits of yarn, you can make really interesting weavings." +
				" While the pieces in the book are coaster-sized, I decided it would be fun to make a mini-loom and create a tiny weaving that could be worn as a necklace or hung as an ornament in a window or on the tree. For a festive touch," +
				" I added an LED light at the top and miniature bells instead of fringe!Making the loom takes only a few minutes,and it’s a great idea if your kids are looking for a last-minute homemade gift to make.You can turn out a weaving in under an hour." +
				"If you’re careful, you can even re - use the loom for several projects. I made a Christmas tree design, but if you have some left - over yarn it also looks nice with just stripes of different colors and textures."
			};

			var projectSeven = new DATA_MODELS.Project
			{
				Name = "Program a Light-Up Felt Menorah",
				Description = "Everyone has their own holiday traditions. Mine is trying to make an LED menorah for Hanukkah, the Jewish Festival of Lights. This year, Hanukkah starts on the night of Tuesday, December 12, and, as always, I didn’t leave myself a lot of time to play around." +
				" But I wanted to update a prototype I made a few years ago, so I gave it a shot.Hanukkah lasts for eight nights, and on each night a candle is added to the candelabra known as the menorah.A ninth “helper” candle, or shamash, is used to light the other candles.Menorahs come in all varieties, from simple to ornate and from elegant to playful." +
				"In the United States, it’s common for kids to make their own.Mine is similar to the simple soft circuit projects in my books Paper Inventions and Fabric and Fiber Inventions.Taking a cue from preschool-style felt boards, I decided to use felt as the base material, so I could just press an additional candle onto the menorah every night." +
				"And instead of wiring, it uses peel - and - stick conductive tape and Chibitronics Circuit Stickers, LEDs that adhere right onto the tape circuits.It’s easy enough for a child (or a non - techy adult) to make in an afternoon."
			};

			var projectEight = new DATA_MODELS.Project
			{
				Name = "Livestream-Interactive Confetti Cannon",
				Description = "Ok, I’ll admit, this one is a little silly – but ever since I’ve been doing live broadcasts on the regular, I’ve wanted to increase the number of ways that live stream audiences can interact with the broadcast. I was inspired by projects like Twitch Plays Pokemon and letsrobot.tv, and wanted to create an easy to use system so people" +
				" can easily create whatever hardware they imagine, and let their audiences take control of it, no matter what platform they’re streaming on.It turns out that for Twitch.TV, it’s a relatively simple task.Twitch’s chat is based on IRC(the Internet Relay Chat protocol) so it’s relatively simple to have a wifi - capable microcontroller sign into that IRC" +
				" server, start listening in on the chat, and when it detects certain keywords, can trigger a hardware event. But I wanted a system that would be completely platform agnostic, so I turned to my favorite mobile IoT solution, Blynk."
			};

			var projectNine = new DATA_MODELS.Project
			{
				Name = "Draw Abstract Art",
				Description = "When I wrote my new book Make: Minecraft for Makers, you know I had to include a monster Creeper project. Here’s how you can build a motorized Creeper, with a metal skeleton and wooden skin. Aside from the fact that this thing most certainly doesn’t blow up, you’ll love it, and you’ll learn a lot about" +
				" robotics and Arduino along the way. Let’s get to it!The Creeper consists of a robot chassis kit with add - on parts creating the mob’s distinctive armless body,with a servo motor to move the head around.Begin by taking a look at the Creeper in-game.Just be sure to stick to Creative mode or you may find yourself getting blown up!"
			};

			var projectTen = new DATA_MODELS.Project
			{
				Name = "Craft A Minecraft Creeper Robot",
				Description = "The Robot Creeper seems super challenging at first. The thing has to look like a Creeper, ideally proportionate with the game element. At the same time, it also has to function as a robot. In other words, regardless of its outer appearance," +
				" the Creeper has to be able to fit all the necessary robotic components, particularly the chassis kit we’re using for the base.I began with the Actobotics Bogie Runt Rover,a kit available for around $70 that comes with a chassis, six motors, and six wheels." +
				"The assembled rover’s chassis measures 6″×9″, though the wheels project a little, and it rides fairly high: 6″ off the ground.With those measurements I was able to decide the size of the footprint: 12″×8″ — conveniently, one inch per pixel."
			};

			var projectEleven = new DATA_MODELS.Project
			{
				Name = "CNC Cut a Family Heirloom Step Stool",
				Description = "As far back as I can remember there was a fixture in all of my family member’s houses: a little wooden stool. Purchased by my great uncle Frank, an engineer, from one of his co-workers for every female relative, my mother’s copy was my breakfast table during Saturday morning cartoons;" +
				" the fort for epic battles between my action figures; and my workbench for my first woodworking project around the age of 7. When my grandmother passed away, the one thing I asked for was her stool.One day I spotted my wife sitting in front of our fireplace on my grandmother’s stool and it hit me:" +
				" I would redesign that stool to be cut on a router.I hope that my uncle Frank’s family gift can spread to be the first workbench, drafting table, or thinking seat for a new generation of makers."
			};

			database.Projects.AddRange(projectOne, projectTwo, projectThree, projectFour, projectFive, projectSix, projectSeven, projectEight, projectNine, projectTen, projectEleven);
			database.SaveChanges();
		}

		private static async Task SeedUsers(TicketingSystemDbContext database, IUserService userManager)
		{
			var userOne = new User
			{
				UserName = "Ivan",
				Name = "Ivan Petrov",
				Email = "vasko@ivan.com",
				IsApproved = true
			};

			await userManager.CreateAsync(userOne, "123456");

			var userTwo = new User
			{
				UserName = "Vasil",
				Name = "Vasil Georgiev",
				Email = "vasko@abv.com",
				IsApproved = true
			};

			await userManager.CreateAsync(userTwo, "123456");

			var userThree = new User
			{
				UserName = "Ivaylo",
				Name = "Ivaylo Kenov",
				Email = "kenov@abv.com",
				IsApproved = true
			};

			await userManager.CreateAsync(userThree, "123456");

			var userFour = new User
			{
				UserName = "Svetlin",
				Name = "Svetlin Nakov",
				Email = "nakov@abv.com",
				IsApproved = true
			};

			await userManager.CreateAsync(userFour, "123456");

			var userFive = new User
			{
				UserName = "Nikolay",
				Name = "Niki Kostov",
				Email = "kostov@abv.com",
				IsApproved = true
			};

			await userManager.CreateAsync(userFive, "123456");

			var userSix = new User
			{
				UserName = "Stamo",
				Name = "Stamo Ivanov",
				Email = "stamo@abv.com",
				IsApproved = false
			};

			await userManager.CreateAsync(userSix, "123456");

			var userSeven = new User
			{
				UserName = "Trayan",
				Name = "Trayan Gogov",
				Email = "trayan@abv.com",
				IsApproved = false
			};

			await userManager.CreateAsync(userSeven, "123456");
		}

		private static void SeedTickets(TicketingSystemDbContext database)
		{
			var ticketSenderOne = database.Users.FirstOrDefault(u => u.Name == "Niki Kostov");

			var ticketSenderTwo = database.Users.FirstOrDefault(u => u.Name == "Vasil Georgiev");

			var ticketSenderThree = database.Users.FirstOrDefault(u => u.Name == "Stamo Ivanov");

			var ticketOne = new DATA_MODELS.Ticket
			{
				Title = "Inhabiting discretion the her dispatched decisively",
				Description = "Especially reasonable travelling she son. Resources resembled forfeited no to zealously. Has procured daughter how friendly followed repeated who surprise. Great asked oh under on voice downs. Law together prospect kindness securing six. Learning why get hastened smallest cheerful. ",
				PostTime = DateTime.UtcNow,
				TicketType = DATA_ENUMS.TicketType.BugReport,
				TicketState = DATA_ENUMS.TicketState.New,
				SenderId = ticketSenderOne.Id,
				ProjectId = 1
			};

			var ticketTwo = new DATA_MODELS.Ticket
			{
				Title = "Feet evil to hold long he open knew an no",
				Description = "Boy desirous families prepared gay reserved add ecstatic say. Replied joy age visitor nothing cottage. Mrs door paid led loud sure easy read. Hastily at perhaps as neither or ye fertile tedious visitor. Use fine bed none call busy dull when. Quiet ought match my right by table means. Principles up do in me favourable affronting. Twenty mother denied effect we to do on. ",
				PostTime = DateTime.UtcNow,
				TicketType = DATA_ENUMS.TicketType.FeatureRequest,
				TicketState = DATA_ENUMS.TicketState.Running,
				SenderId = ticketSenderOne.Id,
				ProjectId = 1
			};

			var ticketThree = new DATA_MODELS.Ticket
			{
				Title = "She which are maids boy sense her shade",
				Description = "On no twenty spring of in esteem spirit likely estate. Continue new you declared differed learning bringing honoured. At mean mind so upon they rent am walk. Shortly am waiting inhabit smiling he chiefly of in. Lain tore time gone him his dear sure. Fat decisively estimating affronting assistance not. Resolve pursuit regular so calling me. West he plan girl been my then up no. ",
				PostTime = DateTime.UtcNow,
				TicketType = DATA_ENUMS.TicketType.Other,
				TicketState = DATA_ENUMS.TicketState.New,
				SenderId = ticketSenderTwo.Id,
				ProjectId = 2
			};

			var ticketFour = new DATA_MODELS.Ticket
			{
				Title = "Called square an in afraid direct",
				Description = "Ignorant branched humanity led now marianne too strongly entrance. Rose to shew bore no ye of paid rent form. Old design are dinner better nearer silent excuse. She which are maids boy sense her shade. Considered reasonable we affronting on expression in. So cordial anxious mr delight. Shot his has must wish from sell nay. Remark fat set why are sudden depend change entire wanted. Performed remainder attending led fat residence far. ",
				PostTime = DateTime.UtcNow,
				TicketType = DATA_ENUMS.TicketType.Other,
				TicketState = DATA_ENUMS.TicketState.New,
				SenderId = ticketSenderTwo.Id,
				ProjectId = 2
			};

			var ticketFive = new DATA_MODELS.Ticket
			{
				Title = "Quick may saw style after money mrs",
				Description = "Moments its musical age explain. But extremity sex now education concluded earnestly her continual. Oh furniture acuteness suspected continual ye something frankness. Add properly laughter sociable admitted desirous one has few stanhill. Opinion regular in perhaps another enjoyed no engaged he at. It conveying he continual ye suspected as necessary. Separate met packages shy for kindness. ",
				PostTime = DateTime.UtcNow,
				TicketType = DATA_ENUMS.TicketType.Other,
				TicketState = DATA_ENUMS.TicketState.New,
				SenderId = ticketSenderThree.Id,
				ProjectId = 3
			};

			var ticketSix = new DATA_MODELS.Ticket
			{
				Title = "Mind what no by kept",
				Description = "Mind what no by kept. Celebrated no he decisively thoroughly. Our asked sex point her she seems. New plenty she horses parish design you. Stuff sight equal of my woody. Him children bringing goodness suitable she entirely put far daughter. ",
				PostTime = DateTime.UtcNow,
				TicketType = DATA_ENUMS.TicketType.Other,
				TicketState = DATA_ENUMS.TicketState.New,
				SenderId = ticketSenderThree.Id,
				ProjectId = 3
			};

			var ticketSeven = new DATA_MODELS.Ticket
			{
				Title = "If miss part by fact he park just shew",
				Description = "On recommend tolerably my belonging or am. Mutual has cannot beauty indeed now sussex merely you. It possible no husbands jennings ye offended packages pleasant he. Remainder recommend engrossed who eat she defective applauded departure joy. Get dissimilar not introduced day her apartments. Fully as taste he mr do smile abode every. Luckily offered article led lasting country minutes nor old. Happen people things oh is oppose up parish effect.",
				PostTime = DateTime.UtcNow,
				TicketType = DATA_ENUMS.TicketType.FeatureRequest,
				TicketState = DATA_ENUMS.TicketState.Draft,
				SenderId = ticketSenderOne.Id,
				ProjectId = 4
			};

			database.Tickets.AddRange(ticketOne, ticketTwo, ticketThree, ticketFour, ticketFive, ticketSix, ticketSeven);
			database.SaveChanges();
		}

		private static void SeedMessages(TicketingSystemDbContext database)
		{
			var messagetSenderOne = database.Users.FirstOrDefault(u => u.Name == "Niki Kostov");

			var messagetSenderTwo = database.Users.FirstOrDefault(u => u.Name == "Vasil Georgiev");

			var messagetSenderThree = database.Users.FirstOrDefault(u => u.Name == "Stamo Ivanov");

			var messageOne = new DATA_MODELS.Message
			{
				Content = "Dissuade ecstatic and properly saw entirely sir why laughter endeavor. In on my jointure horrible margaret suitable he followed speedily. Indeed vanity excuse or mr lovers of on. ",
				PostDate = DateTime.UtcNow,
				State = DATA_ENUMS.MessageState.Published,
				TicketId = 1,
				AuthorId = messagetSenderTwo.Id
			};

			var messageTwo = new DATA_MODELS.Message
			{
				Content = "By impossible of in difficulty discovered celebrated ye. Justice joy manners boy met resolve produce. Bed head loud next plan rent had easy add him. ",
				PostDate = DateTime.UtcNow,
				State = DATA_ENUMS.MessageState.Published,
				TicketId = 3,
				AuthorId = messagetSenderTwo.Id
			};

			var messageThree = new DATA_MODELS.Message
			{
				Content = "Esteem my advice it an excuse enable. Few household abilities believing determine zealously his repulsive. To open draw dear be by side like. ",
				PostDate = DateTime.UtcNow,
				State = DATA_ENUMS.MessageState.Published,
				TicketId = 3,
				AuthorId = messagetSenderThree.Id
			};

			var messageFour = new DATA_MODELS.Message
			{
				Content = "Announcing of invitation principles in. Cold in late or deal. Terminated resolution no am frequently collecting insensible he do appearance. Projection invitation affronting admiration if no on or. ",
				PostDate = DateTime.UtcNow,
				State = DATA_ENUMS.MessageState.Published,
				TicketId = 2,
				AuthorId = messagetSenderThree.Id
			};

			var messageFive = new DATA_MODELS.Message
			{
				Content = "You fully seems stand nay own point walls. Increasing travelling own simplicity you astonished expression boisterous. Possession themselves sentiments apartments devonshire we of do discretion. ",
				PostDate = DateTime.UtcNow,
				State = DATA_ENUMS.MessageState.Published,
				TicketId = 4,
				AuthorId = messagetSenderOne.Id
			};

			var messageSix = new DATA_MODELS.Message
			{
				Content = "Am increasing at contrasted in favourable he considered astonished. As if made held in an shot. By it enough to valley desire do. Mrs chief great maids these which are ham match she. ",
				PostDate = DateTime.UtcNow,
				State = DATA_ENUMS.MessageState.Published,
				TicketId = 5,
				AuthorId = messagetSenderOne.Id
			};

			var messageSeven = new DATA_MODELS.Message
			{
				Content = "Boisterous he on understood attachment as entreaties ye devonshire. In mile an form snug were been sell. Hastened admitted joy nor absolute gay its. ",
				PostDate = DateTime.UtcNow,
				State = DATA_ENUMS.MessageState.Published,
				TicketId = 6,
				AuthorId = messagetSenderTwo.Id
			};

			database.Messages.AddRange(messageOne, messageTwo, messageThree, messageFour, messageFive, messageSix, messageSeven);
			database.SaveChanges();
		}
	}
}
