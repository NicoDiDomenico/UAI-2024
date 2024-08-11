  // Agregacion - El objeto que tenga el rol mayor será el contenedor del resto de objetos (componentes)
  const company = {
    name: "fazt tech",
    employees: []
  }
  
  class Person {
    constructor(name, lastname) {
      this.name = name
      this.lastname = lastname
    }
  }
  
  const john = new Person('john', 'ray')
  const maria = new Person('maria', 'perez')
  
  company.employees.push(john)
  company.employees.push(maria)
  
  console.log(maria)
  console.log(john)
  
  console.log(company)
  