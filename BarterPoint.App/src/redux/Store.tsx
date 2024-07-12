import {configureStore} from "@reduxjs/toolkit"
import productsReducer from "./slices/ProductSlice"
import bidsReducer from "./slices/BidSlice"
import usersReducer from "./slices/UserSlice"

const store = configureStore({
  reducer: {
    products: productsReducer,
    bids: bidsReducer,
    users: usersReducer,
  },
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch
export default store
