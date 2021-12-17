interface IClientViewModel {
  id: number;
  name: string;
  contact: string;
  phone: string;
}

function echo(vm: IClientViewModel) {
  console.log(vm.name);
}

echo({
  id: 1,
  name: "PIZZA SUITE, INC.",
  contact: "HELLO",
  phone: "555-555-5555"
});
