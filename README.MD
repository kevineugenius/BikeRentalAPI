This project contains a little bit of Microsoft boilerplate and little bit of sandbox code that can be ignored.  The BikeRentalController is what we are interested in.  

Hour.csv has the dummy data.

Running the project will open a couple endpoints that are of interest.  The controller LoadData() -> buildFromStrArr() method has a value that shortens the data to just a few results so it is easier to see.  Changing the '5' to 'data.Count' will give 17k results instead of 5.

To view results in the browser or API tool:
https://localhost:44304/api/BikeRental/Get2?request=rentals

To get raw data:
https://localhost:44304/api/BikeRental/GetAll

To test without the file being involved:
https://localhost:44304/api/BikeRental/Get
