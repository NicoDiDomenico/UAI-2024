// Encapsulación - concentrar datos y funcionalidad ocultando detalles internos.
function Company(name) {
    let employees = []; /* esta variable al no tener this, no se puede acceder a ella... */
    this.name = name;
  
    this.getEmployees = function() {
      return employees;
    }
  
    this.addEmployee = function(employee) { /* ...pero para darle uso hacemos una funcion que interactue con ella. */
      employees.push(employee);
    }
  }
  
  const company = new Company("coca cola");
  const company2 = new Company("pepsi");
  
  console.log(company);
  console.log(company2);
  
  company.addEmployee({name: "ryan"}); // notar como le paso un objeto para que loa gregue al arreglo 'privado'
  company2.addEmployee({name: "joe"});
  
  console.log(company.getEmployees());
  console.log(company2.getEmployees());
  


