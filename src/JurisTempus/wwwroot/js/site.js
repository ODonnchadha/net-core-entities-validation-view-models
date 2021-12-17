class ClientViewModel {
  constructor(data) {
    this.id = data.id;
    this.name = data.name;
    this.contact = data.contact;
    this.phone = data.phone;
  }
}

let client = new ClientViewModel({
  id: 1,
  name: "Pizza Suite",
  contact: "Mr. J. Pizza",
  phone : "555-555-5555"
});

client.i
