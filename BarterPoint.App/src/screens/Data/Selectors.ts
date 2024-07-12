import {createSelector} from '@reduxjs/toolkit';
import {RootState} from './Store';

export const selectAllProducts = createSelector(
  (state: RootState) => state.products.userProducts,
  (state: RootState) => state.products.products,
  (userProducts, products) => [...userProducts, ...products]
);