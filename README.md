 # Requirements
 The city needs help with the implementations of a new parking garage and asks you for your support.

 The garage should support different types of vehicles:
 - Cars
 - Motorbikes

 Every vehicle has a unique identifier (this licence plate), and can exist only once - thus being either within the garage or outside of it.

 The planned garage should support multiple parking levels – the city is currently undecided how high they will be able to build for stability reasons. As a result, your implementation should allow for arbitrary numbers of parking levels – at least 1 level, but keep it flexible.

 The same goes for the number of parking spaces per level – the area where the garage will be built is not yet decided upon. So again, keep this flexible and configurable.

 Your task is to develop a simulation program for the garage. Vehicles should be able to enter and exit the garage – the garage should then assign a free space or reject the vehicle if there are no more free parking lots.
 # Assumption
- The parking spot will be organized by row. 
- Each level has multiple rows, each row has multiple parking spots.
- The motorbike takes one parking spot.
- The car takes three parking spots
 # Solution
 ## For flexible configuration for level and parking spots, I used the following configuration.
 ```JSON
"LevelConfiguration": {
    "Total": 5,
    "RowPerLevel": 2,
    "SpotPerRow": 5
}
 ```
- Total: The total of levels that we have in the parking lot.
- RowPerLevel: The rows per level
- SpotPerRow: The spot  per row
 # References
- https://www.andiamogo.com/S-OOD.pdf
- https://fanloyu.gitbooks.io/for_the_interview/content/ood.html
- https://github.com/YongGY/leetcode/blob/master/src/W/OOD/parking/Parking.java
- https://www.cnblogs.com/tobeabetterpig/p/9934270.html
- https://aaronice.gitbook.io/object-oriented-design/ood-case-studies/parking-lot
- https://medium.com/double-pointer/system-design-interview-parking-lot-system-ff2c58167651