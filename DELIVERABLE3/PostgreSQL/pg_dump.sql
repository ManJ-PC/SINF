--
-- PostgreSQL database dump
--

-- Dumped from database version 10.0
-- Dumped by pg_dump version 10.0

-- Started on 2017-12-17 19:31:39

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

SET search_path = public, pg_catalog;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- TOC entry 197 (class 1259 OID 16468)
-- Name: users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE users (
    id integer NOT NULL,
    email character varying(50) NOT NULL,
    password_hash character varying(256) NOT NULL,
    primavera_id character varying(12) NOT NULL
);


ALTER TABLE users OWNER TO postgres;

--
-- TOC entry 198 (class 1259 OID 16471)
-- Name: Users_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE "Users_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE "Users_id_seq" OWNER TO postgres;

--
-- TOC entry 2812 (class 0 OID 0)
-- Dependencies: 198
-- Name: Users_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE "Users_id_seq" OWNED BY users.id;


--
-- TOC entry 199 (class 1259 OID 16473)
-- Name: objectives; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE objectives (
    id integer NOT NULL,
    type character varying(10) NOT NULL,
    limit_value double precision NOT NULL,
    representative_id integer NOT NULL
);


ALTER TABLE objectives OWNER TO postgres;

--
-- TOC entry 200 (class 1259 OID 16476)
-- Name: objectives_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE objectives_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE objectives_id_seq OWNER TO postgres;

--
-- TOC entry 2813 (class 0 OID 0)
-- Dependencies: 200
-- Name: objectives_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE objectives_id_seq OWNED BY objectives.id;


--
-- TOC entry 2678 (class 2604 OID 16478)
-- Name: objectives id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY objectives ALTER COLUMN id SET DEFAULT nextval('objectives_id_seq'::regclass);


--
-- TOC entry 2677 (class 2604 OID 16479)
-- Name: users id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY users ALTER COLUMN id SET DEFAULT nextval('"Users_id_seq"'::regclass);


--
-- TOC entry 2806 (class 0 OID 16473)
-- Dependencies: 199
-- Data for Name: objectives; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO objectives (id, type, limit_value, representative_id) VALUES (2, 'products', 11, 1);
INSERT INTO objectives (id, type, limit_value, representative_id) VALUES (1, 'clients', 11, 1);
INSERT INTO objectives (id, type, limit_value, representative_id) VALUES (3, 'earnings', 2000, 1);


--
-- TOC entry 2804 (class 0 OID 16468)
-- Dependencies: 197
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO users (id, email, password_hash, primavera_id) VALUES (1, 'filipedias@demosinf.pt', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', '1');


--
-- TOC entry 2814 (class 0 OID 0)
-- Dependencies: 198
-- Name: Users_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('"Users_id_seq"', 2, true);


--
-- TOC entry 2815 (class 0 OID 0)
-- Dependencies: 200
-- Name: objectives_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('objectives_id_seq', 1, false);


--
-- TOC entry 2680 (class 2606 OID 16481)
-- Name: users Users_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY users
    ADD CONSTRAINT "Users_pkey" PRIMARY KEY (id);


--
-- TOC entry 2682 (class 2606 OID 16483)
-- Name: objectives objectives_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY objectives
    ADD CONSTRAINT objectives_pkey PRIMARY KEY (id);


-- Completed on 2017-12-17 19:31:39

--
-- PostgreSQL database dump complete
--

