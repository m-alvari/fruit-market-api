import { Injectable } from "@nestjs/common";
import { CreateProduct, Product } from "src/products/models";

@Injectable()
export class ProductService {
  constructor() {
    this.products.push(
      {
      id: 1708966378040,
      name: "dragon",
      price: 100,
      imageUrl: "./assets/images/p6.jpg",
    },
    {
      id: 1708966378041,
      name: "blueberry",
      price: 200,
      imageUrl: "./assets/images/p3.jpg",
    },
    {
      id: 1708766377041,
      name: "lemon",
      price: 700,
      imageUrl: "./assets/images/p1.jpg",
    },
    {
      id: 1708966377441,
      name: "sour cherry",
      price: 145,
      imageUrl: "./assets/images/p8.jpg",
    },
    {
      id: 1708866377041,
      name: "orange",
      price: 85,
      imageUrl: "./assets/images/p7.jpg",
    },
    {
      id: 1708966317041,
      name: "strawberry",
      price: 32,
      imageUrl: "./assets/images/p10.jpg",
    },
    {
      id: 1708966977041,
      name: "banana",
      price: 78,
      imageUrl: "./assets/images/p2.jpg",
    },
    {
      id: 1708986977041,
      name: "cherry",
      price: 12,
      imageUrl: "./assets/images/p12.jpg",
    },
    {
      id: 1708986977041,
      name: "raspberries",
      price: 10,
      imageUrl: "./assets/images/p13.jpg",
    },
    {
      id: 1708986977041,
      name: "Pomegranate",
      price: 15,
      imageUrl: "./assets/images/p11.jpg",
    },
    {
      id: 1708986977041,
      name: "blackberry",
      price: 14,
      imageUrl: "./assets/images/p9.jpg",
    },
    );
  }
  private readonly products: Product[] = [];

  create(product: CreateProduct): Product {
    const item = { ...product, id: new Date().getTime() };
    this.products.push(item);
    return item;
  }

  findAll(): Product[] {
    return this.products;
  }
}
