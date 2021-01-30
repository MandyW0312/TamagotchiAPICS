Problem:

Create a console app that interacts with the API that:

- Allows the user to see all the Pets
- Select a Pet to take care of and then Play with, Scold, or Feed that Pet
- Create a New Pet
- Delete a Pet

Examples:

Create a New Pet, named Nebula
Delete Vision, since he is dead
Select Thor to take care of and Feed him
Select Gamora to take care of and Scold her
Select Nebula to take care of and Play with her

Data Structure:

Pet (Class)
Id: int
Name: string
Birthday: DateTime
HungerLevel: int
HappinessLevel: int

Playtime (Class)
Id: int
When: DateTime
PetId: int
Pet TheAssociated Pet {get; set;}

Feeding (Class)
Id: int
When: DateTime
PetId: int
Pet TheAssociated Pet {get; set;}

Scolding (Class)
Id: int
When: DateTime
PetId: int
Pet TheAssociated Pet {get; set;}

Algorithm:

While keep going equals true
Console.Clear();
Write out Menu Options:
View All Pets
Select A Pet
Create A Pet
Delete A Pet
Quit
Read answer and set to a variable

if choice equals View All Pets
Create a new client list
response as stream code with API URL
List <Pet> pets = jsonSerializer
Read out how many pets are in API
Create a table with Pet's Id, Name, Birthday, Happiness Level, Hunger Level
foreach pet in pets
add a row to the table with info
Write table in minimal format

if choice equals Select A Pet
Ask User which Pet they want to Select by Id
Read the answer and set to variable
Ask the User if they want to play with, feed, or scold the Pet
Read the answer and set to variable

if choice equals Delete A Pet
Ask User which Pet they want to Delete by Id
Read the answer and set to variable
Create a new client list
Set the url to a variable
DeleteAsync the Pet

if choice equals Create A Pet
Ask the User What is the Name of the New Pet
Read Answer and set it to a variable
Create the instance of a New Pet
Create a New Client List
var client = new HttpClient();
Set the url to a variable
var jsonBody = JsonSerializer.Serialize(newPet);
var jsonBodyAsContent = new StringContent(jsonBody);
jsonBodyAsContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
var response = await client.PostAsync(url, jsonBodyAsContent);
var responseJson = await response.Content.ReadAsStreamAsync();
var item = await JsonSerializer.DeserializeAsync<Pet>(responseJson);
Create a table with Pet's Id, Name, Birthday, Happiness Level, Hunger Level
Add a row to the table with info
Write table in minimal format

if choice equals Quit
keep going equals false
