import {Bid} from "./Data/BidSlice"
import {Product} from "../models/Product"
import {ProductCategory} from "../models/ProductCategory"
import {User} from "../models/User"

export const mockUserProducts: Product[] = [
  {
    id: "1001",
    name: "Bicycle",
    description: "A nice road bike.",
    image:
      "https://images.pexels.com/photos/100582/pexels-photo-100582.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2",
    tradeFor: "Electric scooter",
    category: ProductCategory.SportsEquipment,
    condition: "Used",
    location: "Los Angeles, CA",
    ownerId: "user123",
    dimensions: {width: 60, height: 40, depth: 20, weight: 15},
    dateListed: new Date('2023-07-01').toISOString(),
  },
  {
    id: "1002",
    name: "Guitar",
    description: "An acoustic guitar.",
    image:
      "https://images.pexels.com/photos/165971/pexels-photo-165971.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2",
    tradeFor: "Keyboard piano",
    category: ProductCategory.MusicalInstruments,
    condition: "New",
    location: "San Francisco, CA",
    ownerId: "user456",
    dimensions: {width: 40, height: 10, depth: 5, weight: 3},
    dateListed: new Date('2023-06-15').toISOString(),
  },
  {
    id: "1003",
    name: "Laptop",
    description: "A powerful gaming laptop.",
    image:
      "https://images.pexels.com/photos/18105/pexels-photo.jpg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2",
    tradeFor: "High-end smartphone",
    category: ProductCategory.Electronics,
    condition: "Refurbished",
    location: "New York, NY",
    ownerId: "user789",
    dimensions: {width: 15, height: 10, depth: 1, weight: 5},
    dateListed: new Date('2023-05-20').toISOString(),
  },
  {
    id: "1004",
    name: "Camera",
    description: "A DSLR camera.",
    image:
      "https://images.pexels.com/photos/66134/pexels-photo-66134.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2",
    tradeFor: "Drone",
    category: ProductCategory.Electronics,
    condition: "Used",
    location: "Chicago, IL",
    ownerId: "user321",
    dimensions: {width: 8, height: 6, depth: 5, weight: 2},
    dateListed: new Date('2023-04-10').toISOString(),
  },
]

export const mockProducts: Product[] = [
  {
    id: "5",
    name: "Smartphone",
    description: "A handy gadget.",
    image:
      "https://images.pexels.com/photos/607812/pexels-photo-607812.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2",
    tradeFor: "Smartwatch",
    category: ProductCategory.Electronics,
    condition: "Used",
    location: "Miami, FL",
    ownerId: "user987",
    dimensions: {width: 3, height: 6, depth: 0.3, weight: 0.4},
    dateListed: new Date('2023-07-01').toISOString(),
  },
  {
    id: "6",
    name: "Headphones",
    description: "High-quality sound.",
    image:
      "https://images.pexels.com/photos/3394651/pexels-photo-3394651.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2",
    tradeFor: "Speakers",
    category: ProductCategory.Electronics,
    condition: "New",
    location: "Seattle, WA",
    ownerId: "user654",
    dimensions: {width: 7, height: 8, depth: 3, weight: 0.5},
    dateListed: new Date('2023-06-10').toISOString(),
  },
  {
    id: "7",
    name: "Backpack",
    description: "A sturdy backpack.",
    image:
      "https://images.pexels.com/photos/3731256/pexels-photo-3731256.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2",
    tradeFor: "Travel bag",
    category: ProductCategory.SportsEquipment,
    condition: "Used",
    location: "Portland, OR",
    ownerId: "user321",
    dimensions: {width: 15, height: 20, depth: 10, weight: 2},
    dateListed: new Date('2023-07-05').toISOString(),
  },
  {
    id: "8",
    name: "Water Bottle",
    description: "Stay hydrated.",
    image:
      "https://images.pexels.com/photos/1188649/pexels-photo-1188649.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2",
    tradeFor: "Fitness tracker",
    category: ProductCategory.HealthAndBeauty,
    condition: "New",
    location: "Austin, TX",
    ownerId: "user852",
    dimensions: {width: 3, height: 10, depth: 3, weight: 0.5},
    dateListed: new Date('2023-06-20').toISOString(),
  },
  {
    id: "10",
    name: "Gaming Console",
    description: "Enjoy your favorite games.",
    image:
      "https://images.pexels.com/photos/21067/pexels-photo.jpg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2",
    tradeFor: "Virtual Reality Headset",
    category: ProductCategory.Electronics,
    condition: "Used",
    location: "Los Angeles, CA",
    ownerId: "user753",
    dimensions: {width: 12, height: 4, depth: 8, weight: 7},
    dateListed: new Date('2023-05-30').toISOString(),
  },
  {
    id: "11",
    name: "E-Reader",
    description: "Read books on the go.",
    image:
      "https://images.pexels.com/photos/1475276/pexels-photo-1475276.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2",
    tradeFor: "Books",
    category: ProductCategory.Electronics,
    condition: "Refurbished",
    location: "Chicago, IL",
    ownerId: "user369",
    dimensions: {width: 5, height: 7, depth: 0.5, weight: 0.4},
    dateListed: new Date('2023-04-25').toISOString(),
  },
  {
    id: "12",
    name: "Desk Lamp",
    description: "Brighten up your workspace.",
    image:
      "https://images.pexels.com/photos/568290/pexels-photo-568290.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2",
    tradeFor: "Office Chair",
    category: ProductCategory.HomeDecor,
    condition: "New",
    location: "New York, NY",
    ownerId: "user159",
    dimensions: {width: 6, height: 18, depth: 6, weight: 2},
    dateListed: new Date('2023-07-08').toISOString(),
  },
  {
    id: "14",
    name: "Bluetooth Speaker",
    description: "Portable sound system.",
    image:
      "https://images.pexels.com/photos/4062514/pexels-photo-4062514.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2",
    tradeFor: "Headphones",
    category: ProductCategory.Electronics,
    condition: "Used",
    location: "Boston, MA",
    ownerId: "user246",
    dimensions: {width: 6, height: 6, depth: 6, weight: 1.5},
    dateListed: new Date('2023-06-12').toISOString(),
  }
]

export const mockPostProducts: Product[] = [
  {
    id: "105",
    name: "Notebook",
    description: "Keep your notes.",
    tradeFor: "E-book reader",
    image: "https://images.pexels.com/photos/1187344/pexels-photo-1187344.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2",
    category: ProductCategory.OfficeSupplies,
    condition: "New",
    location: "Dallas, TX",
    ownerId: "user111",
    dimensions: {width: 8, height: 11, depth: 0.5, weight: 1},
    dateListed: new Date('2023-06-05').toISOString(),
  },
  {
    id: "106",
    name: "Pen",
    description: "For smooth writing.",
    tradeFor: "Stylus pen",
    image: "https://images.pexels.com/photos/5088179/pexels-photo-5088179.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2",
    category: ProductCategory.OfficeSupplies,
    condition: "New",
    location: "San Jose, CA",
    ownerId: "user112",
    dimensions: {width: 0.5, height: 5, depth: 0.5, weight: 0.05},
    dateListed: new Date('2023-06-10').toISOString(),
  },
  {
    id: "107",
    name: "Sunglasses",
    description: "Protect your eyes.",
    tradeFor: "Hat",
    image: "https://images.pexels.com/photos/46710/pexels-photo-46710.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2",
    category: ProductCategory.HealthAndBeauty,
    condition: "Used",
    location: "Las Vegas, NV",
    ownerId: "user113",
    dimensions: {width: 6, height: 2, depth: 2, weight: 0.1},
    dateListed: new Date('2023-06-20').toISOString(),
  },
  {
    id: "109",
    name: "Charger",
    description: "Stay charged.",
    tradeFor: "Power bank",
    image: "https://images.pexels.com/photos/914912/pexels-photo-914912.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2",
    category: ProductCategory.Electronics,
    condition: "New",
    location: "Denver, CO",
    ownerId: "user114",
    dimensions: {width: 2, height: 3, depth: 1, weight: 0.2},
    dateListed: new Date('2023-06-25').toISOString(),
  },
  {
    id: "114",
    name: "Air Conditioner",
    description: "Cool down your home.",
    image: "https://images.pexels.com/photos/1374448/pexels-photo-1374448.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2",
    tradeFor: "Heater",
    category: ProductCategory.Appliances,
    condition: "Used",
    location: "Phoenix, AZ",
    ownerId: "user115",
    dimensions: {width: 20, height: 15, depth: 10, weight: 30},
    dateListed: new Date('2023-07-01').toISOString(),
  },
  {
    id: "115",
    name: "Vacuum Cleaner",
    description: "Keep your floors clean.",
    image: "https://images.pexels.com/photos/4107286/pexels-photo-4107286.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2",
    tradeFor: "Carpet Cleaner",
    category: ProductCategory.Appliances,
    condition: "New",
    location: "Philadelphia, PA",
    ownerId: "user116",
    dimensions: {width: 10, height: 40, depth: 10, weight: 8},
    dateListed: new Date('2023-07-05').toISOString(),
  },
  {
    id: "116",
    name: "Refrigerator",
    description: "Keep your food fresh.",
    image: "https://images.pexels.com/photos/4003794/pexels-photo-4003794.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2",
    tradeFor: "Freezer",
    category: ProductCategory.Appliances,
    condition: "Used",
    location: "Houston, TX",
    ownerId: "user117",
    dimensions: {width: 36, height: 72, depth: 30, weight: 200},
    dateListed: new Date('2023-06-15').toISOString(),
  },
  {
    id: "117",
    name: "Microwave Oven",
    description: "Heat your food quickly.",
    image: "https://images.pexels.com/photos/5591849/pexels-photo-5591849.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2",
    tradeFor: "Toaster Oven",
    category: ProductCategory.Appliances,
    condition: "New",
    location: "San Diego, CA",
    ownerId: "user118",
    dimensions: {width: 20, height: 12, depth: 16, weight: 25},
    dateListed: new Date('2023-06-30').toISOString(),
  },
  {
    id: "118",
    name: "Dishwasher",
    description: "Effortless dish cleaning.",
    image: "https://images.pexels.com/photos/4107305/pexels-photo-4107305.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2",
    tradeFor: "Washing Machine",
    category: ProductCategory.Appliances,
    condition: "Refurbished",
    location: "Miami, FL",
    ownerId: "user119",
    dimensions: {width: 24, height: 34, depth: 24, weight: 100},
    dateListed: new Date('2023-07-08').toISOString(),
  },
  {
    id: "119",
    name: "Blender",
    description: "Blend your favorite drinks.",
    image: "https://images.pexels.com/photos/8845075/pexels-photo-8845075.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2",
    tradeFor: "Food Processor",
    category: ProductCategory.Appliances,
    condition: "New",
    location: "Dallas, TX",
    ownerId: "user120",
    dimensions: {width: 7, height: 15, depth: 7, weight: 5},
    dateListed: new Date('2023-07-04').toISOString(),
  },
  {
    id: "120",
    name: "Coffee Maker",
    description: "Brew your morning coffee.",
    image: "https://images.pexels.com/photos/714563/pexels-photo-714563.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2",
    tradeFor: "Espresso Machine",
    category: ProductCategory.Appliances,
    condition: "Used",
    location: "Orlando, FL",
    ownerId: "user121",
    dimensions: {width: 8, height: 12, depth: 8, weight: 6},
    dateListed: new Date('2023-07-02').toISOString(),
  },
  {
    id: "121",
    name: "Electric Kettle",
    description: "Boil water quickly.",
    image: "https://images.pexels.com/photos/7176004/pexels-photo-7176004.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2",
    tradeFor: "Tea Maker",
    category: ProductCategory.Appliances,
    condition: "New",
    location: "Atlanta, GA",
    ownerId: "user122",
    dimensions: {width: 7, height: 10, depth: 7, weight: 2},
    dateListed: new Date('2023-07-06').toISOString(),
  }
]

export const mockBids: Bid[] = [
  {product1Id: '5', product2Id: '6'}, // Smartphone <-> Headphones
  {product1Id: '7', product2Id: '8'}, // Backpack <-> Water Bottle
  {product1Id: '10', product2Id: '11'}, // Gaming Console <-> E-Reader
  {product1Id: '12', product2Id: '14'}, // Desk Lamp <-> Bluetooth Speaker
]

export const mockAppLogo = "https://images.pexels.com/photos/247929/pexels-photo-247929.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2"

export const mockProfileImage = "https://images.pexels.com/photos/1310522/pexels-photo-1310522.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2"

export const mockCurrentUser: User = {
  "id": "user12",
  "username": "victoriaclark",
  "email": "victoria.clark@example.com",
  "name": "Victoria Clark",
  "location": "Austin, TX",
  "dateJoined": "2023-07-02T00:00:00"
}