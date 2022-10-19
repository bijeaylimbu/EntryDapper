CREATE  TABLE tbl_voucher(  voucher_id SERIAL PRIMARY KEY,
                        name VARCHAR(255) NOT NULL  
);

CREATE TABLE tbl_ledger(id SERIAL PRIMARY KEY ,
                         name text NOT NULL ,
                          is_cash bool,
                          voucher_id INTEGER NOT NULL REFERENCES tbl_voucher(voucher_id)
);

CREATE TABLE tbl_entry_debit_or_credit(
                                        entry_id SERIAL PRIMARY KEY,
                                        type text Not Null ,
                                       sub_type text NOT NULL ,
                                       amount double precision,
                                       entry_date date
);

CREATE TABLE tbl_entry(id SERIAL PRIMARY KEY,
                    ledger text NOT NULL ,
                    debit double precision,
                    credit double precision,
                    created_date Date NOT NULL,
                    entry_id INTEGER NOT NULL REFERENCES tbl_entry_debit_or_credit(entry_id)
                    );

CREATE TABLE tbl_transaction_entry(id SERIAL Primary Key ,
                           voucher_id SERIAL NOT NULL ,
                           total double precision NOT NULL 
);
