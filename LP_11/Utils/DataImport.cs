using LP_11.Utils;
using OfficeOpenXml;

public class DataImport
{
	public static bool FromExcel(FileStream fs, string type)
	{
		try
		{
			ClassContext context = new ClassContext();

			ExcelPackage.License.SetNonCommercialPersonal("Name");

			ExcelPackage package = new ExcelPackage(fs);
			ExcelWorksheet excelWorksheet = package.Workbook.Worksheets[0];

			var start = excelWorksheet.Dimension.Start;
			var end = excelWorksheet.Dimension.End;

			for (int row = start.Row + 1; row <= end.Row; row++)
			{
				var value = excelWorksheet.Cells[row, 1];

				if (value != null)
				{
					if (type == "Сотрудники")
					{
						Worker worker = new Worker();
						User user = new User();
						WorkerUser workerUser = new WorkerUser();

						if (context.Workers.Any(u => u.FullName == value.Text)) continue;

						worker.FullName = value.Text;

						Post post = new Post();

						if (context.Posts.Any(u => u.Name == excelWorksheet.Cells[row, 2].Text) == false)
						{
							post = new Post()
							{
								Name = excelWorksheet.Cells[row, 2].Text,
								Salary = int.Parse(excelWorksheet.Cells[row, 3].Text)
							};
							context.Add(post);
						}
						else
						{
							post = context.Posts.Where(u => u.Name == excelWorksheet.Cells[row, 2].Text).First();
						}
						Factory factory = context.Factories.Where(u => u.Name == excelWorksheet.Cells[row, 6].Text).FirstOrDefault();

						if (factory == null)
						{
							Console.WriteLine(excelWorksheet.Cells[row, 6].Text);
							factory = new Factory() { Name = excelWorksheet.Cells[row, 6].Text, Adress = excelWorksheet.Cells[row, 7].Text };
						}


						worker.Post = post;
						worker.Factory = factory;
						worker.PhoneNumber = excelWorksheet.Cells[row, 4].Text;
						worker.Email = excelWorksheet.Cells[row, 5].Text;
						worker.BirthDate = Convert.ToDateTime(excelWorksheet.Cells[row, 8].Text).ToUniversalTime();

						user.Login = worker.Email;
						user.Password = Encryptor.Encrypt<byte[]>(GeneratePassword.Generate(5));

						if (!context.Roles.Any(u => u.Name == "user"))
						{
							user.Role = new Role() { Name = "user" };
						}
						else
						{
							user.Role = context.Roles.Where(u => u.Name == "user").First();
						}

						workerUser.Worker = worker;
						workerUser.User = user;

						context.Attach(workerUser);
					}
					else if (type == "Оборудование")
					{
						Production production = new Production();

						production.Name = value.Text;
						production.Price = int.Parse(excelWorksheet.Cells[row, 2].Text);

						string categoryName = excelWorksheet.Cells[row, 3].Text;
						ProductionCategory? productionCategory = context.ProductionCategories.Where(u => u.Name == categoryName).FirstOrDefault();

						if (productionCategory == null)
						{
							productionCategory = new ProductionCategory { Name = categoryName };
						}
						production.Category = productionCategory;

						context.Attach(production);
					}
					else if (type == "Клиенты")
					{
						Client client = new Client();
						client.Name = value.Text;
						client.INN = excelWorksheet.Cells[row, 2].Text;
						client.Email = excelWorksheet.Cells[row, 3].Text;
						client.PhoneNumber = excelWorksheet.Cells[row, 4].Text;
						client.Adress = excelWorksheet.Cells[row, 5].Text;

						context.Attach(client);
					}
					else if (type == "Характеристики")
					{
						string prodName = value.Text;
						string propName = excelWorksheet.Cells[row, 2].Text;
						string propValue = excelWorksheet.Cells[row, 3].Text;

						Production? prod = context.Productions.Where(u => u.Name == prodName).FirstOrDefault();
						ProductionProperty? prop = context.ProductionProperties.Where(u => u.Name == propName).FirstOrDefault();

						if (prod == null)
						{
							continue;
						}

						if (propName == null)
						{
							prop = new ProductionProperty() { Name = propName };
						}

						ProductionProductionProperty productionProp = new ProductionProductionProperty()
						{
							Production = prod,
							ProductionPropety = prop,
							Value = propValue
						};
						context.ProductionProductionPropertes.Attach(productionProp);

					}
					else if (type == "Оборудование на складах")
					{
						string wareName = value.Text;
						string adress = excelWorksheet.Cells[row, 2].Text;
						string propName = excelWorksheet.Cells[row, 3].Text;
						int count = int.Parse(excelWorksheet.Cells[row, 4].Text);

						Warehouse? ware = context.Warehouses.Where(u => u.Name == wareName).FirstOrDefault();
						Production prod = context.Productions.Where(u => u.Name == propName).FirstOrDefault();

						if (prod == null) continue;

						if (ware == null)
						{
							ware = new Warehouse() { Name = wareName, Adress = adress };
						}

						WarehouseProduct warehouseProduct = new WarehouseProduct() { Warehouse = ware, Production = prod, Count = count };
						context.WarehouseProducts.Attach(warehouseProduct);
					}
					context.SaveChanges();
				}
			}
			return true;
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.ToString());
		}
		return false;
	}
}
