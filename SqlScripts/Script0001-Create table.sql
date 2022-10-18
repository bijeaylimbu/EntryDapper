CREATE  TABLE tbl_voucher(
                        id SERIAL  PRIMARY KEY ,
                        name VARCHAR(255) NOT NULL 
);

CREATE TABLE tbl_ledger(id SERIAL PRIMARY KEY ,
                         name text NOT NULL ,
                          is_cash bool
);

CREATE TABLE tbl_entry(id SERIAL PRIMARY KEY,
                    ledger text NOT NULL ,
                    debit double precision,
                    credit double precision,
                    created_date Date NOT NULL);

CREATE TABLE tbl_transaction_entry(id SERIAL Primary Key ,
                           voucher_id SERIAL NOT NULL ,
                           total double precision NOT NULL 
);

