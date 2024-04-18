# BattleshipCodingTestApplication

# I have added Documentation with .PDF file for the code at location ....\BattleShipCodingTest\Documentation\

#Function app end point:
#Create board - https://priyankajbattleshipcodingtest.azurewebsites.net/api/CreateBoard/{BoardSize}
#Add Ships - https://priyankajbattleshipcodingtest.azurewebsites.net/api/CreateShip/{TotalShips}
#Display Board - https://priyankajbattleshipcodingtest.azurewebsites.net/api/DisplayBoard
#Attack Ship - https://priyankajbattleshipcodingtest.azurewebsites.net/api/AttackShip/{Position} 
#Restart Game - https://priyankajbattleshipcodingtest.azurewebsites.net/api/RestartGame

#Steps to test:
#Step 1 – Call Create board end point from postman and pass the boardsize.
#EX: https://priyankajbattleshipcodingtest.azurewebsites.net/api/CreateBoard/3
#Add Ships - Call Add Ships end point from postman and pass the TotalShips you want to add 
#EX: https://priyankajbattleshipcodingtest.azurewebsites.net/api/CreateShip/1
#Display Board – Call Display Board end point to view position which you want to hit
#Attack Ship – Call Attack Ship end point from postman and pass the Position as displayed on board
#EX: https://priyankajbattleshipcodingtest.azurewebsites.net/api/AttackShip/1A
#Restart Game – If you won the game you need to call this API to restart the game and follow the steps again

#Added Postman Collection with .json file for the api at location ....\BattleShipCodingTest\Documentation\