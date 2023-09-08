// Task 1
function task1() {
    var sentence = "I can eat bananas all day";
    var word = sentence.slice(11, 18);
    word = word.toUpperCase();
    console.log(word);
}

// Task 2
function task2() {
    var cars = ["Saab", "Volvo", "BMW"];
    var bmw = cars[2];
    cars[0] = "Mercedes";
    cars.pop();
    cars.push("Audi");
    var removedCars = cars.splice(1, 2);
    console.log(bmw); 
    console.log("Modified Array:", cars); 
    console.log("Removed Cars:", removedCars);
}

task1();
task2();
