api for calculate car parking fee that can support for many parking.
#
1.parkingController 
  - api for register parking that insert parking information such as parking name ,parking address, email password. 
  - api for login.Because this service can support many parking so you must log in with email and password in order to identify and get your parking fee.
  #
 2.FeeController
  - api for insert and update fee for each parking.Forst, you must login after that can manage your parking fee. 
  - api for insert transaction to insert information such as license plate, enter date and time when car enter to parking.
  - api for calculate and update transaction. this api calculate how long car parks in parking and calculate total fee that customer to pay and update total fee, hours, leave date and time.
 
