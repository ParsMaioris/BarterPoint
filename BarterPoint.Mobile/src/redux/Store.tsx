import {configureStore} from "@reduxjs/toolkit"
import productsReducer from "./slices/ProductSlice"
import bidsReducer from "./slices/BidSlice"
import usersReducer from "./slices/UserSlice"
import transactionReducer from "./slices/TransactionSlice"

const store = configureStore({
  reducer: {
    products: productsReducer,
    bids: bidsReducer,
    users: usersReducer,
    transactions: transactionReducer,
  },
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch
export default store